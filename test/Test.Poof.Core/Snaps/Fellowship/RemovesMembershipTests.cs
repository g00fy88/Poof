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
using Yaapii.Atoms.Enumerable;

namespace Poof.Core.Snaps.Fellowship.Test
{
    public sealed  class RemovesMembershipTests
    {
        [Fact]
        public void RemovesMembership()
        {
            var mem = new TestBuilding();
            new Users(mem).Put("gandalf");
            var team = new Fellowships(mem).New();
            new FellowshipOf(mem, team).Update(
                new Name("gandalf and his boys")
            );
            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Team(team),
                new Owner("gandalf")
            );

            new RemovesMembership(mem, new UserIdentity("gandalf")).Convert(
                new DmRemoveMembership(team)
            );

            Assert.Empty(
                new Memberships(mem).List(new Team.Match(team))
            );
        }

        [Fact]
        public void RejectsIfIdentityIsNoMember()
        {
            var mem = new TestBuilding();
            new Users(mem).Put("gandalf");
            var team = new Fellowships(mem).New();
            new FellowshipOf(mem, team).Update(
                new Name("gandalf and his boys")
            );

            Assert.Throws<InvalidOperationException>(()=>
                new RemovesMembership(mem, new UserIdentity("gandalf")).Convert(
                    new DmRemoveMembership(team)
                )
            );
        }
    }
}
