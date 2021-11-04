using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.User.Test
{
    public sealed class UserTests
    {
        [Fact]
        public void AddsMail()
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            user.Update(
                new Pseudonym("its me I swear")
            );

            Assert.Equal(
                "its me I swear",
                new Pseudonym.Name(user).AsString()
            );
        }
    }
}
