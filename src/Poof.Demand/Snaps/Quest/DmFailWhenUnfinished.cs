using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Quest
{
    public sealed class DmFailWhenUnfinished : DemandEnvelope
    {
        public DmFailWhenUnfinished(string quest) : base(() =>
            new PoofDemand("quest", "configuration", "fail-unfinished")
                .Refined("quest", quest)
        )
        { }
    }
}
