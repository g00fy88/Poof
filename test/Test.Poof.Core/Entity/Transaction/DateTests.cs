using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Transaction.Test
{
    public sealed class DateTests
    {
        [Fact]
        public void HasMinValueByDefault()
        {
            var mem = new TestBuilding();
            var transaction =
                new TransactionOf(
                    mem,
                    new Transactions(mem).New()
                );

            Assert.Equal(
                DateTime.MinValue,
                new Date.Of(transaction).Value()
            );
        }

        [Fact]
        public void RetrievesDate()
        {
            var mem = new TestBuilding();
            var transaction =
                new TransactionOf(
                    mem,
                    new Transactions(mem).New()
                );

            var date = DateTime.Now;
            transaction.Update(new Date(date));

            Assert.Equal(
                date,
                new Date.Of(transaction).Value()
            );
        }
    }
}
