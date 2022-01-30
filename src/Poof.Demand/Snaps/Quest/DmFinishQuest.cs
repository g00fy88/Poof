using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Poof.Talk.Snaps;
using Yaapii.JSON;

namespace Poof.Demand.Snaps.Quest
{
    public sealed class DmFinishQuest : DemandEnvelope
    {
        public DmFinishQuest(string quest, string note) : base(()=>
            new PoofDemand("quest", "configuration", "finish-quest", 
                new JSONOf(
                    new JObject(
                        new JProperty("note", note)
                    )
                )
            ).Refined("quest", quest)
        )
        { }
    }
}
