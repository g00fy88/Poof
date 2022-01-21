using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class NoteTests
    {
        [Fact]
        public void UpdatesDescription()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Note("I was not able to do it")
            );

            Assert.Equal(
                "I was not able to do it",
                new Note.Of(quest).AsString()
            );
        }
    }
}
