using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Transaction.Test
{
    public sealed class TitleTests
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
                new Title("ein deal den er nicht ablehnen kann")
            );

            Assert.Equal(
                "ein deal den er nicht ablehnen kann",
                new Title.Of(transaction).AsString()
            );
        }
    }
}
