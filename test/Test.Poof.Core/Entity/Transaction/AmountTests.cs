using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Transaction.Test
{
    public sealed class AmountTests
    {
        [Fact]
        public void HasZeroAmountByDefault()
        {
            var mem = new TestBuilding();
            var transaction =
                new TransactionOf(
                    mem,
                    new Transactions(mem).New()
                );

            Assert.Equal(
                0,
                new Amount.Of(transaction).Value()
            );
        }

        [Fact]
        public void RetrievesAmount()
        {
            var mem = new TestBuilding();
            var transaction =
                new TransactionOf(
                    mem,
                    new Transactions(mem).New()
                );

            transaction.Update(new Amount(12));

            Assert.Equal(
                12,
                new Amount.Of(transaction).Value()
            );
        }
    }
}
