using Poof.Core.Entity.Transaction;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Test
{
    public sealed class TypeTests
    {
        [Fact]
        public void UpdatesTitle()
        {
            var mem = new TestBuilding();
            var transaction =
                new TransactionOf(
                    mem,
                    new Transactions(mem).New()
                );

            transaction.Update(
                new Type("geiler typ")
            );

            Assert.Equal(
                "geiler typ",
                new Type.Of(transaction).AsString()
            );
        }
    }
}
