using Poof.Core.Entity;
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
using Yaapii.Atoms.IO;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Fellowship.Test
{
    public sealed class GetsCatalogTests
    {
        [Theory]
        [InlineData("[0].name", "gandalf and his boys")]
        [InlineData("[0].pictureUrl", "aW1hZ2VieXRlcw==")]
        [InlineData("[0].level", "1")]
        [InlineData("[0].givefactor", "0.5")]
        [InlineData("[0].takefactor", "0.5")]
        [InlineData("[1].name", "aragorn and his boys")]
        [InlineData("[1].pictureUrl", "")]
        public void RetrievesDetails(string jsonpath, string expected)
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var fellowships = new Fellowships(mem);
            var memberships = new Memberships(mem);

            users.Put("gandalf");
            var fellowship1 = fellowships.New();
            var fellowship2 = fellowships.New();
            new FellowshipOf(mem, fellowship1).Update(
                new Name("gandalf and his boys"),
                new Picture(new InputOf("imagebytes"))
            );
            new FellowshipOf(mem, fellowship2).Update(
                new Name("aragorn and his boys")
            );
            new MembershipOf(mem, memberships.New()).Update(
                new Owner("gandalf"),
                new Team(fellowship1),
                new Share(1)
            );
            new MembershipOf(mem, memberships.New()).Update(
                new Owner("gandalf"),
                new Team(fellowship2),
                new Share(1)
            );

            Assert.Equal(
                expected,
                new JSONOf(
                    new GetsCatalog(mem, new FkIdentity("gandalf")).Convert(
                        new DmGetCatalog()
                    ).Result()
                ).Value(jsonpath)
            );
        }
    }
}
