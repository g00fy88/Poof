using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class FinishDateTests
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
                new FinishDate(date)
            );

            Assert.Equal(
                date,
                new FinishDate.Of(quest).Value()
            );
        }
    }
}
