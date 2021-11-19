using Poof.Snaps.Demand;

namespace Poof.Talk.Snaps.User.Configuration
{
    public sealed class DmRemoveFriend : DemandEnvelope
    {
        public DmRemoveFriend(string user) : base(()=>
            new PoofDemand("user", "configuration", "remove-friend")
                .Refined("friend", user)
        )
        { }
    }
}
