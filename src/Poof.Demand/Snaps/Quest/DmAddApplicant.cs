using Poof.Snaps.Demand;
using Poof.Talk.Snaps;

namespace Poof.Demand.Snaps.Quest
{
    public sealed class DmAddApplicant : DemandEnvelope
    {
        public DmAddApplicant(string quest) : base(()=>
            new PoofDemand("quest", "configuration", "add-applicant")
                .Refined("quest", quest)
        )
        { }
    }
}
