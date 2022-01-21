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
    public sealed class GetsDetailsTests
    {
        [Theory]
        [InlineData("name", "gandalf and his boys")]
        [InlineData("pictureUrl", "aW1hZ2VieXRlcw==")]
        [InlineData("level", "3.5")]
        [InlineData("givefactor", "0.49375")]
        [InlineData("takefactor", "0.5")]
        [InlineData("members[0].id", "gandalf")]
        [InlineData("members[0].pseudonym", "the grey")]
        [InlineData("members[0].pseudonumber", "9000")]
        [InlineData("members[0].pictureUrl", "aW1hZ2VieXRlcw==")]
        [InlineData("members[0].level", "3.6666666666666665")]
        [InlineData("members[1].id", "frodo")]
        [InlineData("members[1].pseudonym", "the small")]
        [InlineData("members[1].pseudonumber", "1")]
        [InlineData("members[1].pictureUrl", "aW1hZ2VieXRlcw==")]
        [InlineData("members[1].level", "3.3333333333333335")]
        public void RetrievesDetails(string jsonpath, string expected)
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var fellowships = new Fellowships(mem);
            var memberships = new Memberships(mem);

            users.Put("gandalf");
            users.Put("frodo");
            new UserOf(mem, "gandalf").Update(
                new Pseudonym("the grey", 9000),
                new Picture(new InputOf("imagebytes")),
                new Points(20),
                new BalanceScore(50)
            );
            new UserOf(mem, "frodo").Update(
                new Pseudonym("the small", 1),
                new Picture(new InputOf("imagebytes")),
                new Points(-10),
                new BalanceScore(40)
            );

            var fellowship = fellowships.New();
            new FellowshipOf(mem, fellowship).Update(
                new Name("gandalf and his boys"),
                new Picture(new InputOf("imagebytes"))
            );
            new MembershipOf(mem, memberships.New()).Update(
                new Owner("gandalf"),
                new Team(fellowship),
                new Share(1)
            );
            new MembershipOf(mem, memberships.New()).Update(
               new Owner("frodo"),
               new Team(fellowship),
               new Share(1)
           );

            Assert.Equal(
                expected,
                new JSONOf(
                    new GetsDetails(mem, new UserIdentity("gandalf")).Convert(
                        new DmGetDetails(fellowship)
                    ).Result()
                ).Value(jsonpath)
            );
        }
    }
}
