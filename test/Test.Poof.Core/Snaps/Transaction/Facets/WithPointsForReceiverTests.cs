using Poof.Core.Entity.Fellowship;
using Poof.Core.Entity.Membership;
using Poof.Core.Entity.Transaction;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Pulse;
using Poof.DB.Test;
using Poof.Snaps;
using Poof.Talk.Snaps.Transaction;
using Pulse;
using Pulse.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace Poof.Core.Snaps.Transaction.Facets.Test
{
    public sealed class WithPointsForReceiverTests
    {
        [Fact]
        public void AddsPointsToUser()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();

            new WithPointsForReceiver(mem, new FkPulse(), new UserIdentity(meUser), new FkSnap<IInput>()).Convert(
                new DmAddUserTransaction(otherUser, "title", 34.56)
            );

            Assert.Equal(
                34.56,
                new Points.Of(
                    new UserOf(mem, otherUser)
                ).Value()
            );
        }

        [Fact]
        public void SendsStatusChangedSignal()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();

            ISignal result = new SignalOf(new SigHead("", "", ""));
            new WithPointsForReceiver(mem, new FkPulse(sig => result = sig), new UserIdentity(meUser), new FkSnap<IInput>()).Convert(
                new DmAddUserTransaction(otherUser, "title", 34.56)
            );

            Assert.Equal(
                $"True - {otherUser} - transaction",
                $"{new SigStatusChanged.Is(result).Value()} - {new SigStatusChanged.User(result).AsString()} - {new SigStatusChanged.Name(result).AsString()}"
            );
        }


        [Fact]
        public void AddsBalanceScore()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();

            new UserOf(mem, meUser).Update(
                new Points(-100)
            );
            new UserOf(mem, otherUser).Update(
                new Points(300)
            );
            new WithPointsForReceiver(mem, new FkPulse(), new UserIdentity(meUser), new FkSnap<IInput>()).Convert(
                new DmAddUserTransaction(otherUser, "title", 34.56)
            );

            Assert.Equal(
                17.28,
                new BalanceScore.Total(
                    new UserOf(mem, otherUser)
                ).Value()
            );
        }

        [Fact]
        public void AddTransactionsToMembers()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var member1 = users.New();
            var member2 = users.New();

            var fellowship = new Fellowships(mem).New();

            var memberships = new Memberships(mem);
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(member1),
                new Team(fellowship),
                new Share(1)
            );
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(member2),
                new Team(fellowship),
                new Share(1)
            );

            new WithPointsForReceiver(mem, new FkPulse(), new UserIdentity(meUser), new FkSnap<IInput>()).Convert(
                new DmAddUserTransaction("fellowship", fellowship, "title", 34.56)
            );

            Assert.Equal(
                2,
                new Transactions(mem).List().Count
            );
        }

        [Fact]
        public void AddUserPoints()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var member1 = users.New();
            var member2 = users.New();

            var fellowship = new Fellowships(mem).New();

            var memberships = new Memberships(mem);
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(member1),
                new Team(fellowship),
                new Share(1)
            );
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(member2),
                new Team(fellowship),
                new Share(1)
            );

            new WithPointsForReceiver(mem, new FkPulse(), new UserIdentity(meUser), new FkSnap<IInput>()).Convert(
                new DmAddUserTransaction("fellowship", fellowship, "title", 34.56)
            );

            Assert.Equal(
                new ManyOf<double>(17.28, 17.28),
                new Mapped<string, double>(
                    user => new Points.Of(new UserOf(mem, user)).Value(),
                    new ManyOf(member1, member2)
                )
            );
        }
    }
}
