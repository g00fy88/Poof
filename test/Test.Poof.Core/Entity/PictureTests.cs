using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Test
{
    public sealed class PictureTests
    {
        [Fact]
        public void Adds()
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            user.Update(
                new Picture(
                    new InputOf("#forTheGram")
                )
            );

            Assert.Equal(
                "#forTheGram",
                new TextOf(
                    new Picture.Of(user)
                ).AsString()
            );
        }

        [Fact]
        public void RetrievesBase64Url()
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            var base64Text = new TextBase64("#forTheGram").AsString();
            user.Update(
                new Picture(
                    new InputOf(
                        new Base64Bytes(
                            new BytesOf(
                                new InputOf(base64Text)
                            )
                        )
                    )
                )
            );

            Assert.Equal(
                base64Text,
                new Picture.Base64Url(user).AsString()
            );
        }
    }
}
