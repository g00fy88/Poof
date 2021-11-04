using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Fellowship.Test
{
    public sealed class NameTests
    {
        [Fact]
        public void AddsMail()
        {
            var mem = new TestBuilding();
            var fellowship =
                new FellowshipOf(
                    mem,
                    new Fellowships(mem).New()
                );

            fellowship.Update(
                new Name("SamAndCo")
            );

            Assert.Equal(
                "SamAndCo",
                new Name.Of(fellowship).AsString()
            );
        }
    }
}
