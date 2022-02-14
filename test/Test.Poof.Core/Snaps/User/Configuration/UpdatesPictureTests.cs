using Poof.Core.Entity;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Snaps;
using Poof.Talk.Snaps.User.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Poof.Core.Snaps.User.Configuration.Test
{
    public sealed class UpdatesPictureTests
    {
        [Fact]
        public void UpdatesPicture()
        {
            var mem = new TestBuilding();
            var id = new Users(mem).New();

            new UpdatesPicture(mem, new UserIdentity(id)).Convert(
                new DmUpdatePicture(
                    new InputOf("#forTheGram")
                )
            );

            Assert.Equal(
                "#forTheGram",
                new TextOf(
                    new Picture.Of(new UserOf(mem, id))
                ).AsString()
            );
        }
    }
}
