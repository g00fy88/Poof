using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class CompletionTimeTests
    {
        [Fact]
        public void UpdatesTime()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );
            var date = DateTime.Now.AddDays(7);
            quest.Update(
                new CompletionTime(48.5)
            );

            Assert.Equal(
                48.5,
                new CompletionTime.Of(quest).Value()
            );
        }
    }
}
