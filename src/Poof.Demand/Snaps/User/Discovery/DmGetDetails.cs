using Poof.Snaps.Demand;

namespace Poof.Talk.Snaps.User.Discovery
{
    public sealed class DmGetDetails : DemandEnvelope
    {
        public DmGetDetails() : base(()=>
            new PoofDemand("user", "discovery", "get-details")
        )
        { }
    }
}
