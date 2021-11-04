using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Fellowship.Test
{
    public sealed class FellowshipsTests
    {
        [Fact]
        public void AddsFellowship()
        {
            var mem = new TestBuilding();
            var catalog = new Fellowships(mem);
            catalog.Put("new-fellowship");

            Assert.Contains(
                "new-fellowship",
                catalog.List()
            );
        }

        [Fact]
        public void RemovesTransaction()
        {
            var mem = new TestBuilding();
            var catalog = new Fellowships(mem);
            catalog.Put("new-fellowship");
            catalog.Remove("new-fellowship");

            Assert.Empty(
                catalog.List()
            );
        }
    }
}
