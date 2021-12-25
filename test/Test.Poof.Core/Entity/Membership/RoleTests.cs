using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Membership.Test
{
    public sealed class RoleTests
    {
        [Fact]
        public void AddsRole()
        {
            var mem = new TestBuilding();
            var membership =
                new MembershipOf(
                    mem,
                    new Memberships(mem).New()
                );

            membership.Update(
                new Role("admin")
            );

            Assert.Equal(
                "admin",
                new Role.Of(membership).AsString()
            );
        }
    }
}
