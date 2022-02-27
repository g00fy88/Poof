using Poof.Core.Entity.Friendship;
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
            var fs = new Friendships(mem);
            var me = users.New();
            var friend1 = users.New();
            var friend2 = users.New();

            new UserOf(mem, me).Update(
                new Pseudonym("batman", 0)
            );
            new UserOf(mem, friend1).Update(
                new Pseudonym("robin", 0)
            );
            new UserOf(mem, friend2).Update(
                new Pseudonym("robin", 1)
            );

            new FriendshipOf(mem, fs.New()).Update(
                new Requester(me),
                new Friend(friend1)
            );
            var fs2 = fs.New();
            new FriendshipOf(mem, fs2).Update(
                new Requester(me),
                new Friend(friend2)
            );

            new RemovesFriend(
                mem,
                new UserIdentity(me)
            ).Convert(
                new DmRemoveFriend(fs2)
            );

            Assert.Equal(
                1,
                fs.List(new Requester.Match(me)).Count
            );
        }
    }
}
