using Poof.Core.Entity.Quest;
using Poof.Core.Entity.User;
using Poof.Core.Model;
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
    public sealed class GetsCatalogTests
    {
        [Theory]
        [InlineData("[0].scope", "public")]
        [InlineData("[0].status", "open")]
        [InlineData("[0].category", "party")]
        [InlineData("[0].title", "project x revival")]
        [InlineData("[0].description", "party like theres no tomorrow")]
        [InlineData("[0].applicant.has", "True")]
        [InlineData("[0].applicant.name", "geralt")]
        [InlineData("[0].endDate.has", "True")]
        [InlineData("[0].location.has", "True")]
        [InlineData("[0].location.value", "ground zero")]
        [InlineData("[0].contact.has", "True")]
        [InlineData("[0].reward", "40")]
        [InlineData("[0].completionTime", "48")]
        public void RetrievesValues(string jsonPath, string expected)
        {
            var mem = new TestBuilding();
            var user = new Users(mem).New();
            var otherUser = new Users(mem).New();
            new UserOf(mem, otherUser).Update(
                new Pseudonym("geralt", 0)
            );
            var quests = new Quests(mem);

            new QuestOf(mem, quests.New()).Update(
                new Scope("public"),
                new Status("open"),
                new Issuer(otherUser),
                new EndDate(DateTime.Now.AddDays(5)),
                new Applicant(otherUser),
                new Category("party"),
                new Title("project x revival"),
                new Description("party like theres no tomorrow"),
                new Location(true, "ground zero"),
                new Contact(false, ""),
                new Reward(40),
                new CompletionTime(48)
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
    }
}
