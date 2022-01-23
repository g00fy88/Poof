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

namespace Poof.Core.Snaps.Quest.Test
{
    public sealed class AddsWeeklyQuestsTests
    {
        [Fact]
        public void RemovesOpenAndExpiredPrivateQuests()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var quests = new Quests(mem);

            new QuestOf(mem, quests.New()).Update(
                new Scope("public"),
                new Status("open"),
                new Issuer(user),
                new EndDate(DateTime.Now.AddDays(1))
            );
            new QuestOf(mem, quests.New()).Update(
                new Scope("public"),
                new Status("open"),
                new Issuer(user),
                new EndDate(DateTime.Now.AddDays(-1))
            );
            new QuestOf(mem, quests.New()).Update(
                new Scope("private"),
                new Status("open"),
                new Issuer(user),
                new EndDate(DateTime.Now.AddDays(1))
            );
            new QuestOf(mem, quests.New()).Update(
                new Scope("private"),
                new Status("open"),
                new Issuer(user),
                new EndDate(DateTime.Now.AddDays(-1))
            );
            new QuestOf(mem, quests.New()).Update(
               new Scope("private"),
               new Status("applied"),
               new Issuer(user),
               new EndDate(DateTime.Now.AddDays(-1))
            );

            new AddsWeeklyQuests(mem, new UserIdentity(user), new FkFuture()).Convert(
                new DmAddWeeklyQuests()
            );

            Assert.Equal(
                4,
                quests.List().Count
            );
        }

        [Fact]
        public void ReschedulesToEndDateOfOpenPrivate()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var quests = new Quests(mem);

            var expected = DateTime.Now.AddDays(1);
            new QuestOf(mem, quests.New()).Update(
                new Scope("private"),
                new Status("open"),
                new Issuer(user),
                new EndDate(expected)
            );

            var result = DateTime.Now;
            new AddsWeeklyQuests(
                mem, 
                new UserIdentity(user), 
                new FkFuture(
                    ()=> { },
                    (date, job) => result = date
                )
            ).Convert(
                new DmAddWeeklyQuests()
            );

            Assert.Equal(
                expected,
                result
            );
        }

        [Fact]
        public void ReschedulesTo7Days()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var quests = new Quests(mem);

            new QuestOf(mem, quests.New()).Update(
                new Scope("private"),
                new Status("open"),
                new Issuer(user),
                new EndDate(DateTime.Now.AddDays(-1))
            );

            var result = DateTime.Now;
            new AddsWeeklyQuests(
                mem,
                new UserIdentity(user),
                new FkFuture(
                    () => { },
                    (date, job) => result = date
                )
            ).Convert(
                new DmAddWeeklyQuests()
            );

            Assert.True(
                result.Subtract(DateTime.Now.AddDays(7)).TotalSeconds < 5
            );
        }
    }
}
