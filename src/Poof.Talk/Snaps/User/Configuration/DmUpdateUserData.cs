using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

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
