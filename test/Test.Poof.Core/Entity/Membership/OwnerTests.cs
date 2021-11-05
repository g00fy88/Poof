using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Membership.Test
{
    public sealed class OwnerTests
    {
        [Fact]
        public void AddsOwner()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var membership =
                new MembershipOf(
                    mem,
                    new Memberships(mem).New()
                );

            membership.Update(
                new Owner(user)
            );

            Assert.Equal(
                user,
                new Owner.Of(membership).AsString()
            );
        }

        [Fact]
        public void CannotAddNotExistentUser()
        {
            var mem = new TestBuilding();
            var membership =
                new MembershipOf(
                    mem,
                    new Memberships(mem).New()
                );

            Assert.Throws<InvalidOperationException>(()=>
                membership.Update(
                    new Owner("not-existent-user")
                )
            );
        }
    }
}
