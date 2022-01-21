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
using Yaapii.JSON;

namespace Poof.Core.Snaps.Transaction.Test
{
    public sealed class GetsUserTransactionsTests
    {
        [Fact]
        public void GetsUserTransactions()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var meUser = users.New();
            var otherUser = users.New();
            var thirdUser = users.New();

            new UserOf(mem, meUser).Update(
                new Pseudonym("hello-its-me", 1)
            );
            new UserOf(mem, otherUser).Update(
                new Pseudonym("hello-its-you", 2)
            );
            new UserOf(mem, thirdUser).Update(
                new Pseudonym("hello-its-someone", 3)
            );

            var transactions = new Transactions(mem);

            new TransactionOf(mem, transactions.New()).Update(
                new Title("youAndMe"),
                new TakeSide("user", meUser),
                new GiveSide("user", otherUser),
                new Date(DateTime.Now),
                new Amount(20)
            );
            new TransactionOf(mem, transactions.New()).Update(
                new Title("MeAndSomeone"),
                new TakeSide("user", thirdUser),
                new GiveSide("user", meUser),
                new Date(DateTime.Now),
                new Amount(20)
            );
            new TransactionOf(mem, transactions.New()).Update(
                new Title("youAndSomeone"),
                new TakeSide("user", otherUser),
                new GiveSide("user", thirdUser),
                new Date(DateTime.Now),
                new Amount(20)
            );

            Assert.Equal(
                2,
                new JSONOf(
                    new GetsUserTransactions(mem, new UserIdentity(meUser)).Convert(
                        new DmGetUserTransactions()
                    ).Result()
                ).Nodes("[*]").Count
            );
        }
    }
}
