using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Transaction.Test
{
    public sealed class TransactionsTests
    {
        [Fact]
        public void AddsTransaction()
        {
            var mem = new TestBuilding();
            var catalog = new Transactions(mem);
            catalog.Put("new-transaction");

            Assert.Contains(
                "new-transaction",
                catalog.List()
            );
        }

        [Fact]
        public void RemovesTransaction()
        {
            var mem = new TestBuilding();
            var catalog = new Transactions(mem);
            catalog.Put("new-transaction");
            catalog.Remove("new-transaction");

            Assert.Empty(
                catalog.List()
            );
        }
    }
}
