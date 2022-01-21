using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class DescriptionTests
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
                new Description("an unimportant side story")
            );

            Assert.Equal(
                "an unimportant side story",
                new Description.Of(quest).AsString()
            );
        }
    }
}
