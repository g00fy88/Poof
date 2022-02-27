using Poof.Snaps.Demand;

namespace Poof.Talk.Snaps.User.Configuration
{
    public sealed class DmConfirmFriend : DemandEnvelope
    {
        public DmConfirmFriend(string friendship) : base(()=>
            new PoofDemand("user", "configuration", "confirm-friend")
                .Refined("friendship", friendship)
        )
        { }
    }
}
