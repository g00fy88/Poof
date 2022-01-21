using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class EndDateTests
    {
        [Fact]
        public void UpdatesDate()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );
            var date = DateTime.Now.AddDays(7);
            quest.Update(
                new EndDate(date)
            );

            Assert.Equal(
                date,
                new EndDate.Of(quest).Value()
            );
        }

        [Fact]
        public void HasDate()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new EndDate(DateTime.Now.AddDays(7))
            );

            Assert.True(
                new EndDate.Has(quest).Value()
            );
        }

        [Fact]
        public void DoesNotHasDate()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            Assert.False(
                new EndDate.Has(quest).Value()
            );
        }
    }
}
