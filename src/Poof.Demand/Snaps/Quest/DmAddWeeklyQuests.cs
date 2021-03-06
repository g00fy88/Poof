using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Quest
{
    public sealed class DmAddWeeklyQuests : DemandEnvelope
    {
        public DmAddWeeklyQuests(string user) : base(() =>
            new PoofDemand("quest", "configuration", "add-weeklies")
                .Refined("user", user)
        )
        { }
    }
}
