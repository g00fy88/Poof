using Newtonsoft.Json.Linq;
using Poof.Core.Entity.Quest;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;

namespace Poof.Core.Snaps.Quest
{
    public sealed class GetsCatalog : SnapEnvelope<IInput>
    {
        public GetsCatalog(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var quests =
                new Joined<string>(
                    new Quests(mem).List(
                        new Issuer.Match(identity.UserID())
                    ),
                    new Quests(mem).List(
                        new Scope.Match("public"),
                        new Status.Match("open"),
                        new Issuer.NoMatch(identity.UserID())
                    ),
                    new Quests(mem).List(
                        new Scope.Match("public"),
                        new Applicant.Match(identity.UserID()),
                        new Issuer.NoMatch(identity.UserID())
                    )
                );

            var result = new JArray();
            foreach(var id in quests)
            {
                var quest = new QuestOf(mem, id);
                var issuerId = new Issuer.Of(quest).Value();
                var issuer = new UserOf(mem, issuerId);
                var hasApplicant = new Applicant.Has(quest).Value();
                var hasEndDate = new EndDate.Has(quest).Value();
                var needsLocation = new Location.Needed(quest).Value();
                var hasPicture = new Picture.Has(quest).Value();

                result.Add(
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("scope", new Scope.Of(quest).AsString()),
                        new JProperty("status", new Status.Of(quest).AsString()),
                        new JProperty("issuer",
                            new JObject(
                                new JProperty(id, issuerId),
                                new JProperty("name", new Pseudonym.Name(issuer).AsString())
                            )
                        ),
                        new JProperty("applicant",
                            new JObject(
                                new JProperty("has", hasApplicant),
                                new JProperty("me", hasApplicant ?
                                    new Applicant.Of(quest).Value() == identity.UserID() :
                                    false
                                ),
                                new JProperty("name", hasApplicant ?
                                    new Pseudonym.Name(new UserOf(mem, new Applicant.Of(quest).Value())).AsString() :
                                    ""
                                ),
                                new JProperty("startDate", hasApplicant ?
                                    new Applicant.StartDate(quest).Value().ToString("u", CultureInfo.InvariantCulture) :
                                    ""
                                )
                            )
                        ),
                        new JProperty("completionTime", new TextOf(new CompletionTime.Of(quest).Value()).AsString()),
                        new JProperty("reward", new TextOf(new Reward.Of(quest).Value()).AsString()),
                        new JProperty("factor", new TextOf(new Points.TakeFactor(issuer).Value()).AsString()),
                        new JProperty("category", new Category.Of(quest).AsString()),
                        new JProperty("title", new Title.Of(quest).AsString()),
                        new JProperty("description", new Description.Of(quest).AsString()),
                        new JProperty("note", new Note.Of(quest).AsString()),
                        new JProperty("endDate",
                            new JObject(
                                new JProperty("has", hasEndDate),
                                new JProperty("value", hasEndDate ? 
                                    new EndDate.Of(quest).Value().ToString(CultureInfo.InvariantCulture) :
                                    ""
                                )
                            )
                        ),
                        new JProperty("location",
                            new JObject(
                                new JProperty("has", needsLocation),
                                new JProperty("value", needsLocation ? new Location.Of(quest).AsString() : "")
                            )
                        ),
                        new JProperty("picture",
                            new JObject(
                                new JProperty("has", hasPicture),
                                new JProperty("url", hasPicture ? new Picture.Url(quest).AsString() : "")
                            )
                        )
                    )
                );
            }

            return new JsonRawOutcome(result);
        })
        {

        }
    }
}
