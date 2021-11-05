using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Snaps;
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
                new FkIdentity(user)
            ).Convert(
                new EmptyDemand().Refined("pseudonym", "pseudoman")
            );

            Assert.Equal(
                "pseudoman",
                new Pseudonym.Name(new UserOf(mem, user)).AsString()
            );
        }
    }
}
