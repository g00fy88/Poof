using Poof.Core.Entity.Quest;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Future;
using Poof.DB.Test;
using Poof.Snaps;
using Poof.Talk.Snaps.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaapii.Atoms.Scalar;

namespace Poof.Core.Snaps.Quest.Test
{
    public sealed class RemovesUnappliedTests
    {
        [Fact]
        public void Removes()
        {
            var mem = new TestBuilding();
            var quests = new Quests(mem);
            var questId = quests.New();

            var quest = new QuestOf(mem, questId);
            quest.Update(
                new Scope("public"),
                new Status("open"),
                new EndDate(DateTime.Now.AddDays(-1))
            );

            new RemovesUnapplied(mem, new FkFuture()).Convert(
                new DmRemoveUnapplied(questId)
            );

            Assert.Empty(
                new Quests(mem).List()
            );
        }

        [Fact]
        public void ReschedulesWhenNotExpired()
        {
            var mem = new TestBuilding();
            var quests = new Quests(mem);
            var questId = quests.New();

            var quest = new QuestOf(mem, questId);
            var endDate = DateTime.Now.AddDays(1);
            quest.Update(
                new Scope("public"),
                new Status("open"),
                new EndDate(endDate)
            );

            var result = DateTime.Now;
            new RemovesUnapplied(
                mem, 
                new FkFuture(
                    () => { },
                    job => result = job.DueDate()
                )
            ).Convert(
                new DmRemoveUnapplied(questId)
            );

            Assert.Equal(
                endDate,
                result
            );
        }
    }
}
