using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class PictureTests
    {
        [Fact]
        public void DoesNotHaveIfNotAdded()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );


            Assert.False(
                new Picture.Has(quest).Value()
            );
        }

        [Fact]
        public void Adds()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Picture("#forTheGram")
            );

            Assert.Equal(
                "#forTheGram",
                new Picture.Url(quest).AsString()
            );
        }

        [Fact]
        public void RetrievesBase64Url()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            var base64Text = new TextBase64("#forTheGram").AsString();
            quest.Update(
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
                new Picture.Url(quest).AsString()
            );
        }
    }
}
