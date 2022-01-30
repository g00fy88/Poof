using Poof.Core.Entity.Quest;
using Poof.Core.Entity.User;
using Poof.Core.Future;
using Poof.Core.Model;
using Poof.Core.Model.Future;
using Poof.DB.Test;
using Poof.Demand.Snaps.Quest;
using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Quest.Test
{
    public sealed class FinishesQuestTests
    { 
        [Fact]
        public void Finishes()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var other = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("public"),
                new Issuer(other),
                new Applicant(user)
            );

            new FinishesQuest(mem, new UserIdentity(user)).Convert(
                new DmFinishQuest(quest, "I did it")
            );

            Assert.Equal(
                "finished",
                new Status.Of(new QuestOf(mem, quest)).AsString()
            );
        }

        [Fact]
        public void AddsFinishDate()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var other = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("public"),
                new Issuer(other),
                new Applicant(user)
            );

            new FinishesQuest(mem, new UserIdentity(user)).Convert(
                new DmFinishQuest(quest, "I did it")
            );

            Assert.True(
                DateTime.Now.Subtract(new FinishDate.Of(new QuestOf(mem, quest)).Value()).TotalSeconds < 5
            );
        }

        [Fact]
        public void AddsNote()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var other = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("public"),
                new Issuer(other),
                new Applicant(user)
            );

            new FinishesQuest(mem, new UserIdentity(user)).Convert(
                new DmFinishQuest(quest, "I did it")
            );

            Assert.Equal(
                "I did it",
                new Note.Of(new QuestOf(mem, quest)).AsString()
            );
        }

        [Fact]
        public void RejectNoApplicant()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var other = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("public"),
                new Issuer(other)
            );

            Assert.Throws<InvalidOperationException>(()=>
                new FinishesQuest(mem, new UserIdentity(user)).Convert(
                    new DmFinishQuest(quest, "I did it")
                )
            );
        }

        [Fact]
        public void RejectWrongApplicant()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var other = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("public"),
                new Issuer(other),
                new Applicant(other)
            );

            Assert.Throws<InvalidOperationException>(() =>
                new FinishesQuest(mem, new UserIdentity(user)).Convert(
                    new DmFinishQuest(quest, "I did it")
                )
            );
        }
    }
}
