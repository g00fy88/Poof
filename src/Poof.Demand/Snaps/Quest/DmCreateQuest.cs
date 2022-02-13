using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Poof.Talk.Snaps;
using System;
using System.Globalization;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Demand.Snaps.Quest
{
    public sealed class DmCreateQuest : DemandEnvelope
    {
        public DmCreateQuest(
            string scope,
            string category,
            string title,
            string description,
            double reward,
            DateTime endDate,
            double completionHours,
            bool locationNeeded,
            string location,
            bool contactNeeded,
            string contact
        ) : base(()=>
            new PoofDemand("quest", "configuration", "create-quest",
                new JSONOf(
                    new JObject(
                        new JProperty("scope", scope),
                        new JProperty("category", category),
                        new JProperty("title", title),
                        new JProperty("description", description),
                        new JProperty("reward", new TextOf(reward).AsString()),
                        new JProperty("endDate", endDate.ToString(CultureInfo.InvariantCulture)),
                        new JProperty("completionTime", new TextOf(completionHours).AsString()),
                        new JProperty("location",
                            new JObject(
                                new JProperty("has", locationNeeded),
                                new JProperty("value", location)
                            )
                        ),
                        new JProperty("contact",
                            new JObject(
                                new JProperty("has", contactNeeded),
                                new JProperty("value", contact)
                            )
                        )
                    )
                )
            )
        )
        { }
    }
}
