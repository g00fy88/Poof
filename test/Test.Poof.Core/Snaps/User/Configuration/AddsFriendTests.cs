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
    public sealed class AddsFriendTests
    {
        [Fact]
        public void Adds()
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

            new AddsFriend(
                mem,
                new UserIdentity(me)
            ).Convert(
                new DmAddFriend(friend2)
            );

            Assert.Equal(
                2,
                fs.List(new Requester.Match(me)).Count
            );
        }

        [Fact]
        public void DoesNotAddAlreadyExisingFriend()
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

            new AddsFriend(
                mem,
                new UserIdentity(me)
            ).Convert(
                new DmAddFriend(friend1)
            );

            Assert.Single(
                fs.List(new Requester.Match(me))
            );
        }

        [Fact]
        public void RejectsSelfFriending()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var me = users.New();
            var friend1 = users.New();

            new UserOf(mem, me).Update(
                new Pseudonym("batman", 0)
            );
            new UserOf(mem, friend1).Update(
                new Pseudonym("robin", 0)
            );

            Assert.Throws<ArgumentException>(()=>
                new AddsFriend(
                    mem,
                    new UserIdentity(me)
                ).Convert(
                    new DmAddFriend(me)
                )
            );
        }
    }
}
