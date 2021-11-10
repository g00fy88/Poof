using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Snaps;
using Poof.Talk.Snaps.User.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaapii.JSON;

namespace Poof.Core.Snaps.User.Discovery.Test
{
    public sealed class FindsByNameTests
    {
        [Fact]
        public void FiltersUsers()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            new UserOf(mem, users.New()).Update(
                new Pseudonym("batman", 0)
            );
            new UserOf(mem, users.New()).Update(
                new Pseudonym("robin", 0)
            );
            new UserOf(mem, users.New()).Update(
                new Pseudonym("robin", 1)
            );

            Assert.Equal(
                2,
                new AwFindByName.List(
                    new FindsByName(mem, new FkIdentity()).Convert(
                        new DmFindByName("robin")
                    )
                ).Count
            );
        }

        [Fact]
        public void DoesNotReturnSelf()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var user = users.New();
            new UserOf(mem, user).Update(
                new Pseudonym("batman", 0)
            );

            Assert.Empty(
                new AwFindByName.List(
                    new FindsByName(mem, new FkIdentity(user)).Convert(
                        new DmFindByName("batman")
                    )
                )
            );
        }
    }
}
