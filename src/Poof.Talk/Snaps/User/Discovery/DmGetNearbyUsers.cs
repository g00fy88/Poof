using Poof.Snaps.Demand;

namespace Poof.Talk.Snaps.User.Discovery
{
    public sealed class DmGetNearbyUsers : DemandEnvelope
    {
        public DmGetNearbyUsers() : base(
            new PoofDemand("user", "discovery", "get-nearby-users")
        )
        { }
    }
}
