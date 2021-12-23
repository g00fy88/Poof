using Poof.Core.Entity.Transaction;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Pulse;
using Poof.DB.Test;
using Poof.Talk.Snaps.Transaction;
using Pulse;
using Pulse.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Poof.Core.Snaps.Transaction.Test
{
    public sealed class AddsUserTransactionTests 
    {
        [Fact]
        public void AddsNewTransaction()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();

            new AddsUserTransaction(mem, new FkIdentity(meUser)).Convert(
                new DmAddUserTransaction(otherUser, "title",  34.56)
            );

            Assert.NotEmpty(
                new Transactions(mem).List()
            );
        }

        [Fact]
        public void RemovesPointsFromSender()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();

            new AddsUserTransaction(mem,  new FkIdentity(meUser)).Convert(
                new DmAddUserTransaction(otherUser, "title", 34.56)
            );

            Assert.Equal(
                -34.56,
                new Points.Of(
                    new UserOf(mem, meUser)
                ).Value()
            );
        }

        [Fact]
        public void RejectsNegativeAmount()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();

            Assert.Throws<ArgumentException>(()=>
                new AddsUserTransaction(mem, new FkIdentity(meUser)).Convert(
                    new DmAddUserTransaction(otherUser, "title", -34.56)
                )
            );
        }
    }
}
