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
    public sealed class AddsApplicantTests
    {
        [Fact]
        public void AddsApplicant()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var other = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("public"),
                new Issuer(other)
            );

            new AddsApplicant(mem, new UserIdentity(user), new FkFuture()).Convert(
                new DmAddApplicant(quest)
            );

            Assert.Equal(
                user,
                new Applicant.Of(new QuestOf(mem, quest)).Value()
            );
        }

        [Fact]
        public void AddsStatus()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var other = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("public"),
                new Issuer(other)
            );

            new AddsApplicant(mem, new UserIdentity(user), new FkFuture()).Convert(
                new DmAddApplicant(quest)
            );

            Assert.Equal(
                "pending",
                new Status.Of(new QuestOf(mem, quest)).AsString()
            );
        }

        [Fact]
        public void SchedulesJob()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var other = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("public"),
                new Issuer(other)
            );

            IJob job = new JobOf(new EmptyDemand());
            new AddsApplicant(
                mem, 
                new UserIdentity(user),
                new FkFuture(
                    () => { },
                    j => job = j
                )
            ).Convert(
                new DmAddApplicant(quest)
            );

            Assert.Equal(
                $"fail-unfinished-{quest}",
                $"{job.Demand().Param("action")}-{job.Demand().Param("quest")}"
            );
        }

        [Fact]
        public void RejectsPrivateQuest()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var other = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("private"),
                new Issuer(other)
            );

            Assert.Throws<InvalidOperationException>(()=>
                new AddsApplicant(mem, new UserIdentity(user), new FkFuture()).Convert(
                    new DmAddApplicant(quest)
                )
            );
        }

        [Fact]
        public void AddsOnPrivateWithMatchingIssuer()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var quest = new Quests(mem).New();

            new QuestOf(mem, quest).Update(
                new Scope("private"),
                new Issuer(user)
            );

            new AddsApplicant(mem, new UserIdentity(user), new FkFuture()).Convert(
                new DmAddApplicant(quest)
            );

            Assert.Equal(
                user,
                new Applicant.Of(new QuestOf(mem, quest)).Value()
            );
        }
    }
}
