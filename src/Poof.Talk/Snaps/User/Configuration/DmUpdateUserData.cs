using Poof.Snaps.Demand;

namespace Poof.Talk.Snaps.User.Configuration
{
    public sealed class DmUpdateUserData : DemandEnvelope
    {
        public DmUpdateUserData(string pseudonym) : base(()=>
            new PoofDemand("user", "configuration", "update-user")
                .Refined("pseudonym", pseudonym)
        )
        { }
    }
}
