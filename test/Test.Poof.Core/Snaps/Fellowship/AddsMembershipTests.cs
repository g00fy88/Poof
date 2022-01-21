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
    public sealed  class AddsMembershipTests
    {
        [Fact]
        public void AddsMembership()
        {
            var mem = new TestBuilding();
            new Users(mem).Put("gandalf");
            new Users(mem).Put("frodo");
            var team = new Fellowships(mem).New();
            new FellowshipOf(mem, team).Update(
                new Name("gandalf and his boys")
            );
            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Team(team),
                new Owner("gandalf")
            );

            new AddsMembership(mem, new UserIdentity("gandalf")).Convert(
                new DmAddMembership(team, "frodo")
            );

            Assert.Contains(
                "frodo",
                new Mapped<string, string>(m =>
                    new Owner.Of(new MembershipOf(mem, m)).AsString(),
                    new Memberships(mem).List(new Team.Match(team))
                )
            );
        }

        [Fact]
        public void RejectsIfIdentityIsNoMember()
        {
            var mem = new TestBuilding();
            new Users(mem).Put("gandalf");
            new Users(mem).Put("frodo");
            var team = new Fellowships(mem).New();
            new FellowshipOf(mem, team).Update(
                new Name("gandalf and his boys")
            );

            Assert.Throws<InvalidOperationException>(()=>
                new AddsMembership(mem, new UserIdentity("gandalf")).Convert(
                    new DmAddMembership(team, "frodo")
                )
            );
        }

        [Fact]
        public void RejectsIfMembershipAlreadyExists()
        {
            var mem = new TestBuilding();
            new Users(mem).Put("gandalf");
            new Users(mem).Put("frodo");
            var team = new Fellowships(mem).New();
            new FellowshipOf(mem, team).Update(
                new Name("gandalf and his boys")
            );
            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Team(team),
                new Owner("gandalf")
            );
            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Team(team),
                new Owner("frodo")
            );

            Assert.Throws<InvalidOperationException>(() =>
                new AddsMembership(mem, new UserIdentity("gandalf")).Convert(
                    new DmAddMembership(team, "frodo")
                )
            );
        }
    }
}
