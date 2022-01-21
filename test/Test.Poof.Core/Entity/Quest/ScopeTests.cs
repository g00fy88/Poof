using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Quest.Test
{
    public sealed class ScopeTests
    {
        [Fact]
        public void UpdatesScope()
        {
            var mem = new TestBuilding();
            var quest =
                new QuestOf(
                    mem,
                    new Quests(mem).New()
                );

            quest.Update(
                new Scope("public")
            );

            Assert.Equal(
                "public",
                new Scope.Of(quest).AsString()
            );
        }
    }
}
