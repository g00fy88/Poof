using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Quest
{
    public sealed class DmRemoveUnapplied : DemandEnvelope
    {
        public DmRemoveUnapplied(string quest) : base(() =>
            new PoofDemand("quest", "configuration", "remove-unapplied")
                .Refined("quest", quest)
        )
        { }
    }
}
