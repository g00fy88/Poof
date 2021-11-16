using Poof.Core.Entity.Transaction;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Talk.Snaps.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Poof.Core.Snaps.Transaction.Test
{
    public sealed class AddsTransactionTests 
    {
        [Fact]
        public void AddsNewTransaction()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();

            new AddsTransaction(mem, new FkIdentity(meUser)).Convert(
                new DmAddTransaction(otherUser, "title",  34.56)
            );

            Assert.NotEmpty(
                new Transactions(mem).List()
            );
        }

        [Fact]
        public void AddsPointsToUser()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();

            new AddsTransaction(mem, new FkIdentity(meUser)).Convert(
                new DmAddTransaction(otherUser, "title", 34.56)
            );

            Assert.Equal(
                34.56,
                new Points.Of(
                    new UserOf(mem, otherUser)
                ).Value()
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
            new AddsTransaction(mem, new FkIdentity(meUser)).Convert(
                new DmAddTransaction(otherUser, "title", 34.56)
            );

            Assert.Equal(
                17.28,
                new BalanceScore.Total(
                    new UserOf(mem, otherUser)
                ).Value()
            );
        }

        [Fact]
        public void RevertsPointsOnError()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var otherUser = users.New();

            new UserOf(mem, otherUser).Update(new Points(100));
            try
            {
                new AddsTransaction(mem, new FkIdentity()).Convert(
                    new DmAddTransaction(otherUser, "title", 34.56)
                );
            }
            catch(Exception)
            {
                Assert.Equal(
                    100,
                    new Points.Of(new UserOf(mem, otherUser)).Value()
                );
                Assert.Equal(
                    0,
                    new BalanceScore.Total(new UserOf(mem, otherUser)).Value()
                );
            }
        }

        [Fact]
        public void RejectsNegativeAmount()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();

            Assert.Throws<ArgumentException>(()=>
                new AddsTransaction(mem, new FkIdentity(meUser)).Convert(
                    new DmAddTransaction(otherUser, "title", -34.56)
                )
            );
        }
    }
}
