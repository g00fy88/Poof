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

namespace Poof.Core.Snaps.Transaction.Test
{
    public sealed class AddsFellowshipTransactionTests
    {
        [Fact]
        public void AddsMainTransaction()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var buddy = users.New();
            var otherUser = users.New();

            var team = new Fellowships(mem).New();

            var memberships = new Memberships(mem);
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(meUser),
                new Team(team),
                new Share(1)
            );
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(buddy),
                new Team(team),
                new Share(1)
            );

            new AddsFellowshipTransaction(mem, new FkPulse(), new UserIdentity(meUser)).Convert(
                new DmAddFellowshipTransaction(team, "user", otherUser, "title", 34.56)
            );

            Assert.Single(
                new Transactions(mem).List(new Entity.Type.Match("main"))
            );
        }

        [Fact]
        public void AddsMemberTransactions()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var buddy = users.New();
            var otherUser = users.New();

            var team = new Fellowships(mem).New();

            var memberships = new Memberships(mem);
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(meUser),
                new Team(team),
                new Share(1)
            );
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(buddy),
                new Team(team),
                new Share(1)
            );

            new AddsFellowshipTransaction(mem, new FkPulse(), new UserIdentity(meUser)).Convert(
                new DmAddFellowshipTransaction(team, "user", otherUser, "title", 34.56)
            );

            Assert.Equal(
                2,
                new Transactions(mem).List(new Entity.Type.Match("automatic")).Count
            );
        }

        [Fact]
        public void SendsStatusChangedSignal()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var buddy = users.New();
            var otherUser = users.New();

            var team = new Fellowships(mem).New();

            var memberships = new Memberships(mem);
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(meUser),
                new Team(team),
                new Share(1)
            );
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(buddy),
                new Team(team),
                new Share(1)
            );

            var count = 0;
            new AddsFellowshipTransaction(mem, new FkPulse(send:sig => count++), new UserIdentity(meUser)).Convert(
                new DmAddFellowshipTransaction(team, "user", otherUser, "title", 34.56)
            );

            Assert.Equal(
                2,
                count
            );
        }

        [Fact]
        public void TakesMemberPoints()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var buddy = users.New();
            var otherUser = users.New();

            var team = new Fellowships(mem).New();

            var memberships = new Memberships(mem);
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(meUser),
                new Team(team),
                new Share(1)
            );
            new MembershipOf(mem, memberships.New()).Update(
                new Owner(buddy),
                new Team(team),
                new Share(1)
            );

            new AddsFellowshipTransaction(mem, new FkPulse(), new UserIdentity(meUser)).Convert(
                new DmAddFellowshipTransaction(team, "user", otherUser, "title", 34.56)
            );

            Assert.Equal(
                new ManyOf<double>(-17.28, -17.28),
                new Mapped<string, double>(
                    user => new Points.Of(new UserOf(mem, user)).Value(),
                    new ManyOf(meUser, buddy)
                )
            );
        }
    }
}
