using Poof.Core.Entity;
using Poof.Core.Entity.Quest;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Demand.Snaps.Quest;
using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Poof.Core.Snaps.Quest.Configuration.Test
{
    public sealed class UpdatesPictureTests
    {
        [Fact]
        public void UpdatesPicture()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var id = new Quests(mem).New();
            var quest = new QuestOf(mem, id);

            quest.Update(
                new Issuer(user)
            );

            new UpdatesPicture(mem, new UserIdentity(user)).Convert(
                new DmUpdatePicture(
                    id,
                    new InputOf("#forTheGram")
                )
            );

            Assert.Equal(
                $"data:image/jpg;base64,{new TextBase64("#forTheGram").AsString()}",
                new Entity.Quest.Picture.Url(quest).AsString()
            );
        }

        [Fact]
        public void RejectsWrongIdentity()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var id = new Quests(mem).New();
            var quest = new QuestOf(mem, id);

            quest.Update(
                new Issuer(user)
            );

            Assert.Throws<InvalidOperationException>(()=>
                new UpdatesPicture(mem, new UserIdentity("123-otheruser")).Convert(
                    new DmUpdatePicture(
                        id,
                        new InputOf("#forTheGram")
                    )
                )
            );
        }
    }
}
