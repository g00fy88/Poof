using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Membership.Test
{
    public sealed class ShareTests
    {
        [Fact]
        public void AddsMail()
        {
            var mem = new TestBuilding();
            var membership =
                new MembershipOf(
                    mem,
                    new Memberships(mem).New()
                );

            membership.Update(
                new Share(0.1)
            );

            Assert.Equal(
                0.1,
                new Share.Of(membership).Value()
            );
        }
    }
}
