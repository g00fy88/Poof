using Poof.Core.Entity.Fellowship;
using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Membership.Test
{
    public sealed class TeamTests
    {
        [Fact]
        public void AddsTeam()
        {
            var mem = new TestBuilding();
            var fellowship = new Fellowships(mem).New();
            var membership =
                new MembershipOf(
                    mem,
                    new Memberships(mem).New()
                );

            membership.Update(
                new Team(fellowship)
            );

            Assert.Equal(
                fellowship,
                new Team.Of(membership).AsString()
            );
        }

        [Fact]
        public void CannotAddNotExistentTeam()
        {
            var mem = new TestBuilding();
            var membership =
                new MembershipOf(
                    mem,
                    new Memberships(mem).New()
                );

            Assert.Throws<InvalidOperationException>(()=>
                membership.Update(
                    new Team("not-existent-fellowship")
                )
            );
        }
    }
}
