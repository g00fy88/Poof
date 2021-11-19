using Poof.Snaps.Demand;

namespace Poof.Talk.Snaps.User.Configuration
{
    public sealed class DmAddFriend : DemandEnvelope
    {
        public DmAddFriend(string user) : base(()=>
            new PoofDemand("user", "configuration", "add-friend")
                .Refined("friend", user)
        )
        { }
    }
}
