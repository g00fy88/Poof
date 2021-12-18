using Poof.Core.Entity.Fellowship;
using Poof.Core.Entity.Membership;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Talk.Snaps.Fellowship;
using System;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Poof.Core.Snaps.Fellowship.Test
{
    public sealed class UpdatesNameTests
    {
        [Fact]
        public void UpdatesName()
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

            new UpdatesName(mem, new FkIdentity("gandalf")).Convert(
                new DmUpdateName(team, "aragorn and his boys")
            );

            Assert.Equal(
                "aragorn and his boys",
                new Name.Of(new FellowshipOf(mem, team)).AsString()
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
                new UpdatesName(mem, new FkIdentity("gandalf")).Convert(
                    new DmUpdateName(team, "aragorn and his boys")
                )
            );
        }

        [Fact]
        public void RejectsIfNameAlreadyExists()
        {
            var mem = new TestBuilding();
            new Users(mem).Put("gandalf");
            var team = new Fellowships(mem).New();
            new FellowshipOf(mem, team).Update(
                new Name("gandalf and his boys")
            );
            new FellowshipOf(mem, new Fellowships(mem).New()).Update(
                new Name("aragorn and his boys")
            );
            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Team(team),
                new Owner("gandalf")
            );

            Assert.Throws<ArgumentException>(() =>
                 new UpdatesName(mem, new FkIdentity("gandalf")).Convert(
                    new DmUpdateName(team, "aragorn and his boys")
                )
            );
        }
    }
}
