using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Snaps;
using Poof.Talk.Snaps.User.Configuration;
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
    public sealed class GetsFriendsTests
    {
        [Fact]
        public void ListsFriends()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var me = users.New();
            var friend1 = users.New();
            var friend2 = users.New();
            var notAFriend = users.New();

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

            Assert.Equal(
                2,
                new AwGetFriends.List(
                    new GetsFriends(mem, new FkIdentity(me)).Convert(
                        new DmGetFriends()
                    )
                ).Count
            );
        }

        [Fact]
        public void ListsFriendsAfterAdding()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
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

            new AddsFriend(mem, new FkIdentity(me)).Convert(
                new DmAddFriend(friend1)
            );

            Assert.Single(
                new AwGetFriends.List(
                    new GetsFriends(mem, new FkIdentity(me)).Convert(
                        new DmGetFriends()
                    )
                )
            );
        }
    }
}
