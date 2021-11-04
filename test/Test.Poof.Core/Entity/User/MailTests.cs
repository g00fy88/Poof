using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.User.Test
{
    public sealed class MailTests
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
                new Mail("my.super@mail.address")
            );

            Assert.Equal(
                "my.super@mail.address",
                new Mail.Of(user).AsString()
            );
        }
    }
}
