using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.User.Test
{
    public sealed class UsersTests
    {
        [Fact]
        public void AddsUser()
        {
            var mem = new TestBuilding();
            var catalog = new Users(mem);
            catalog.Put("new-user");

            Assert.Contains(
                "new-user",
                catalog.List()
            );
        }

        [Fact]
        public void RemovesUser()
        {
            var mem = new TestBuilding();
            var catalog = new Users(mem);
            catalog.Put("new-user");
            catalog.Remove("new-user");

            Assert.Empty(
                catalog.List()
            );
        }
    }
}
