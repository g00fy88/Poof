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
    public sealed class RemovesFriendTests
    {
        [Fact]
        public void Removes()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var me = users.New();
            var friend1 = users.New();
            var friend2 = users.New();

            new UserOf(mem, me).Update(
                new Pseudonym("batman", 0),
                new Friends(friend1, friend2)
            );
            new UserOf(mem, friend1).Update(
                new Pseudonym("robin", 0)
            );
            new UserOf(mem, friend2).Update(
                new Pseudonym("robin", 1)
            );

            new RemovesFriend(
                mem,
                new FkIdentity(me)
            ).Convert(
                new DmRemoveFriend(friend2)
            );

            Assert.Single(
                new Friends.Of(new UserOf(mem, me))
            );
        }
    }
}
