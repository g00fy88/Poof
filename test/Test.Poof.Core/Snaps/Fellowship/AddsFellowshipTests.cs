using Poof.Core.Entity.Fellowship;
using Poof.Core.Entity.Membership;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Talk.Snaps.Fellowship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Poof.Core.Snaps.Fellowship.Test
{
    public sealed  class AddsFellowshipTests
    {
        [Fact]
        public void AddsFellowship()
        {
            var mem = new TestBuilding();
            new Users(mem).Put("gandalf");
            new AddsFellowship(mem, new FkIdentity("gandalf")).Convert(
                new DmAddFellowship("gandalf and his boys")
            );

            Assert.NotEmpty(
                new Fellowships(mem).List()
            );
        }

        [Fact]
        public void AddsIdentityMembership()
        {
            var mem = new TestBuilding();
            new Users(mem).Put("gandalf");
            new AddsFellowship(mem, new FkIdentity("gandalf")).Convert(
                new DmAddFellowship("gandalf and his boys")
            );

            Assert.Equal(
                "gandalf",
                new Owner.Of(
                    new MembershipOf(mem, new Memberships(mem).List()[0])
                ).AsString()
            );
        }

        [Fact]
        public void RejectsExistingName()
        {
            var mem = new TestBuilding();
            new FellowshipOf(mem, new Fellowships(mem).New()).Update(
                new Name("gandalf and his boys")
            );

            Assert.Throws<ArgumentException>(()=>
                new AddsFellowship(mem, new FkIdentity("gandalf")).Convert(
                    new DmAddFellowship("gandalf and his boys")
                )
            );
        }
    }
}
