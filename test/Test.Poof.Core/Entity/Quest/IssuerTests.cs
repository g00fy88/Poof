using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class IssuerTests
    {
        [Fact]
        public void UpdatesIssuer()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Issuer(user)
            );

            Assert.Equal(
                user,
                new Issuer.Of(quest).Value()
            );
        }
    }
}
