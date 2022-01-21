using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class TitleTests
    {
        [Fact]
        public void UpdatesTitle()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Title("put the baby in the oven")
            );

            Assert.Equal(
                "put the baby in the oven",
                new Title.Of(quest).AsString()
            );
        }
    }
}
