using Newtonsoft.Json.Linq;
using Poof.Core.Entity.Quest;
using Poof.Core.Future;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Future;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using Poof.Talk.Snaps.Quest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Quest
{
    /// <summary>
    /// Creates a new quest with the requesting user as issuer.
    /// Adds the given details to the quest.
    /// Returns the new quest id
    /// </summary>
    public sealed class CreatesQuest : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Creates a new quest with the requesting user as issuer.
        /// Adds the given details to the quest.
        /// Returns the new quest id
        /// </summary>
        public CreatesQuest(IDataBuilding mem, IIdentity identity, IFuture future) : base(dmd =>
        {
            var questId = new Quests(mem).New();
            var quest = new QuestOf(mem, questId);

            var json = new JSONOf(dmd.Body());

            quest.Update(
                new Scope(new Strict(json.Value("scope"), "public").AsString()),
                new Status("open"),
                new Issuer(identity.UserID()),
                new CompletionTime(
                    new DoubleOf(json.Value("completionTime")).Value()
                ),
                new Reward(
                    new DoubleOf(json.Value("reward")).Value()
                ),
                new Category(json.Value("category")),
                new Title(json.Value("title")),
                new Description(json.Value("description")),
                new EndDate(
                    DateTime.Parse(json.Value("endDate"), CultureInfo.InvariantCulture)
                ),
                new Location(
                    new BoolOf(json.Value("location.has")).Value(),
                    json.Value("location.value")
                ),
                new Contact(
                    new BoolOf(json.Value("contact.has")).Value(),
                    json.Value("contact.value")
                )
            );

            future.Schedule(
                new JobOf(
                    new EndDate.Of(quest).Value(),
                    new DmRemoveUnapplied(questId)
                )
            );

            return
                new JsonRawOutcome(
                    new JObject(
                        new JProperty("id", questId)
                    )
                );
        })
        { }
    }
}
