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
    public sealed class FailsWhenUnfinishedTests
    {
        [Fact]
        public void UpdatesStatus()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var otherUser = new Users(mem).New();
            var quests = new Quests(mem);
            var questId = quests.New();

            var quest = new QuestOf(mem, questId);
            quest.Update(
                new Scope("public"),
                new Status("pending"),
                new Issuer(user),
                new EndDate(DateTime.Now.AddDays(1)),
                new Applicant(otherUser, new ScalarOf<DateTime>(()=>DateTime.Now.AddHours(-24))),
                new CompletionTime(23)
            );

            new FailsWhenUnfinished(mem, new FkFuture()).Convert(
                new DmFailWhenUnfinished(questId)
            );

            Assert.Equal(
                "failed",
                new Status.Of(quest).AsString()
            );
        }

        [Fact]
        public void ReschedulesWhenNotExpired()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var otherUser = new Users(mem).New();
            var quests = new Quests(mem);
            var questId = quests.New();

            var quest = new QuestOf(mem, questId);
            quest.Update(
                new Scope("public"),
                new Status("pending"),
                new Issuer(user),
                new EndDate(DateTime.Now.AddDays(1)),
                new Applicant(otherUser),
                new CompletionTime(23)
            );

            var result = DateTime.Now;
            new FailsWhenUnfinished(
                mem, 
                new FkFuture(
                    () => { },
                    job => result = job.DueDate()
                )
            ).Convert(
                new DmFailWhenUnfinished(questId)
            );

            Assert.True(
                result.Subtract(DateTime.Now.AddHours(23)).TotalSeconds < 5
            );
        }
    }
}
