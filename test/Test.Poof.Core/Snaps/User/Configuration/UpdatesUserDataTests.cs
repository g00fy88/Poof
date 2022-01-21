using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Snaps;
using Poof.Talk.Snaps.User.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Snaps.User.Configuration.Test
{
    public sealed class UpdatesUserDataTests
    {
        [Fact]
        public void UpdatesPseudonym()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();

            new UpdatesUserData(
                mem,
                new UserIdentity(user)
            ).Convert(
                new DmUpdateUserData("pseudoman")
            );

            Assert.Equal(
                "pseudoman",
                new Pseudonym.Name(new UserOf(mem, user)).AsString()
            );
        }

        [Fact]
        public void SetsRandomNumber()
        {
            var mem = new TestBuilding();
            var user1= new Users(mem).New();
            var user2 = new Users(mem).New();
            new UserOf(mem, user2).Update(
                new Pseudonym("pseudoman", 1235)
            );

            new UpdatesUserData(
                mem,
                new UserIdentity(user1)
            ).Convert(
                new DmUpdateUserData("pseudoman")
            );

            Assert.NotEqual(
                1235,
                new Pseudonym.Number(new UserOf(mem, user1)).Value()
            );
        }
    }
}
