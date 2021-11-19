using Poof.Snaps.Demand;

namespace Poof.Talk.Snaps.User.Discovery
{
    public sealed class DmGetFriends : DemandEnvelope
    {
        public DmGetFriends() : base(()=>
            new PoofDemand("user", "discovery", "get-friends")
        )
        { }
    }
}
