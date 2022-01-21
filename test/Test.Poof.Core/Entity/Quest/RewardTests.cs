using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class RewardTests
    {
        [Fact]
        public void UpdatesReward()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Reward(10)
            );

            Assert.Equal(
                10,
                new Reward.Of(quest).Value()
            );
        }
    }
}
