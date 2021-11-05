using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Membership.Test
{
    public sealed class MembershipsTests
    {
        [Fact]
        public void AddsMembership()
        {
            var mem = new TestBuilding();
            var catalog = new Memberships(mem);
            catalog.Put("new-membership");

            Assert.Contains(
                "new-membership",
                catalog.List()
            );
        }

        [Fact]
        public void RemovesMembership()
        {
            var mem = new TestBuilding();
            var catalog = new Memberships(mem);
            catalog.Put("new-membership");
            catalog.Remove("new-membership");

            Assert.Empty(
                catalog.List()
            );
        }
    }
}
