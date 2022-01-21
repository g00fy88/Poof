using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class CategoryTests
    {
        [Fact]
        public void UpdatesCategory()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Category("sidequests")
            );

            Assert.Equal(
                "sidequests",
                new Category.Of(quest).AsString()
            );
        }
    }
}
