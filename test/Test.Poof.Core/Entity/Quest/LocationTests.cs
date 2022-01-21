using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class LocationTests
    {
        [Fact]
        public void UpdatesLocation()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );
            quest.Update(
                new Location(true, "under the stone next to the tree")
            );

            Assert.Equal(
                "under the stone next to the tree",
                new Location.Of(quest).AsString()
            );
        }

        [Fact]
        public void HasLocation()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Location(true, "under the stone next to the tree")
            );

            Assert.True(
                new Location.Needed(quest).Value()
            );
        }

        [Fact]
        public void DoesNotHasLocation()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            Assert.False(
                new Location.Needed(quest).Value()
            );
        }
    }
}
