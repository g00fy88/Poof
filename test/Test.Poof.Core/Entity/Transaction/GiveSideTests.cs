using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Transaction.Test
{
    public sealed class GiveSideTests
    {
        [Fact]
        public void AddsSide()
        {
            var mem = new TestBuilding();
            var transaction =
                new TransactionOf(
                    mem,
                    new Transactions(mem).New()
                );

            transaction.Update(
                new GiveSide("user", "123-user")
            );

            Assert.Equal(
                "123-user",
                new GiveSide.Entity(transaction).AsString()
            );
        }

        [Fact]
        public void AddsType()
        {
            var mem = new TestBuilding();
            var transaction =
                new TransactionOf(
                    mem,
                    new Transactions(mem).New()
                );

            transaction.Update(
                new GiveSide("user", "123-user")
            );

            Assert.Equal(
                "user",
                new GiveSide.Type(transaction).AsString()
            );
        }

        [Fact]
        public void RejectsWrongType()
        {
            var mem = new TestBuilding();
            var transaction =
                new TransactionOf(
                    mem,
                    new Transactions(mem).New()
                );

            Assert.Throws<ArgumentException>(()=>
                transaction.Update(
                    new GiveSide("wrong-type", "123-user")
                )
            );
        }
    }
}
