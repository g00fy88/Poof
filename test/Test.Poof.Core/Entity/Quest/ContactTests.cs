using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class ContactTests
    {
        [Fact]
        public void UpdatesContact()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );
            quest.Update(
                new Contact(true, "the guy with the black jacket")
            );

            Assert.Equal(
                "the guy with the black jacket",
                new Contact.Of(quest).AsString()
            );
        }

        [Fact]
        public void HasContact()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Contact(true, "the guy with the black jacket")
            );

            Assert.True(
                new Contact.Needed(quest).Value()
            );
        }

        [Fact]
        public void DoesNotHasContact()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            Assert.False(
                new Contact.Needed(quest).Value()
            );
        }
    }
}
