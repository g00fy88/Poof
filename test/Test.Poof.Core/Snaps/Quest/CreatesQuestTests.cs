using Poof.Core.Entity.Quest;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Future;
using Poof.DB.Test;
using Poof.Demand.Snaps.Quest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Quest.Test
{
    public sealed class CreatesQuestTests
    {
        [Theory]
        [InlineData("[0].scope", "public")]
        [InlineData("[0].category", "party")]
        [InlineData("[0].title", "project x revival")]
        [InlineData("[0].description", "party like theres no tomorrow")]
        [InlineData("[0].endDate.has", "True")]
        [InlineData("[0].location.has", "True")]
        [InlineData("[0].location.value", "ground zero")]
        [InlineData("[0].contact.has", "False")]
        [InlineData("[0].reward", "40")]
        [InlineData("[0].completionTime", "48")]
        public void CreatesQuestWithDetails(string jsonPath, string expected)
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();

            new CreatesQuest(mem, new UserIdentity(user), new FkFuture()).Convert(
                new DmCreateQuest(
                    "public",
                    "party",
                    "project x revival",
                    "party like theres no tomorrow",
                    40,
                    DateTime.Now,
                    48,
                    true,
                    "ground zero",
                    false,
                    ""
                )
            );
            var response =
                new JSONOf(
                    new GetsCatalog(mem, new UserIdentity(user)).Convert(
                        new DmGetCatalog()
                    ).Result()
                );

            Assert.Equal(
                expected,
                response.Value(jsonPath)
            );
        }

        [Fact]
        public void ReturnsNewId()
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();

            var response =
                new CreatesQuest(mem, new UserIdentity(user), new FkFuture()).Convert(
                    new DmCreateQuest(
                        "public",
                        "party",
                        "project x revival",
                        "party like theres no tomorrow",
                        40,
                        DateTime.Now,
                        48,
                        true,
                        "ground zero",
                        false,
                        ""
                    )
                );

            Assert.Equal(
                new Quests(mem).List()[0],
                new JSONOf(response.Result()).Value("id")
            );
        }
    }
}
