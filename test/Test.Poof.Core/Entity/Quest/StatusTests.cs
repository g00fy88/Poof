using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class StatusTests
    {
        [Fact]
        public void UpdatesStatus()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Status("open")
            );

            Assert.Equal(
                "open",
                new Status.Of(quest).AsString()
            );
        }
    }
}
