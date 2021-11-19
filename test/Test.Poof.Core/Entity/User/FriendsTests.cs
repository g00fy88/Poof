using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Enumerable;

namespace Poof.Core.Entity.User.Test
{
    public sealed class FriendsTests
    {
        [Fact]
        public void AddsFriends()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var user = new UserOf(mem, users.New());

            user.Update(
                new Friends(
                    users.New(),
                    users.New()
                )
            );
            Assert.Equal(
                2,
                new Friends.Of(user).Count
            );
        }

        [Fact]
        public void OverwritesFriends()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var user = new UserOf(mem, users.New());

            user.Update(
                new Friends(
                    users.New(),
                    users.New()
                )
            );

            var newList = new ManyOf(users.New(), users.New(), users.New());

            user.Update(new Friends(newList));

            Assert.Equal(
                newList,
                new Friends.Of(user)
            );
        }
    }
}
