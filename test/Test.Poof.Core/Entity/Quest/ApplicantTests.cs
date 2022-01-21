using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class ApplicantTests
    {
        [Fact]
        public void UpdatesApplicant()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Applicant(user)
            );

            Assert.Equal(
                user,
                new Applicant.Of(quest).Value()
            );
        }

        [Fact]
        public void HasApplicant()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Applicant(user)
            );

            Assert.True(
                new Applicant.Has(quest).Value()
            );
        }

        [Fact]
        public void DoesNotHasApplicant()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            Assert.False(
                new Applicant.Has(quest).Value()
            );
        }
    }
}
