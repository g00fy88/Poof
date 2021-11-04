using System;
using System.Collections.Generic;
using System.Text;
using Poof.Snaps.Demand;

namespace Poof.Talk.Snaps.User.Registration
{
    public sealed class DmSignsUp : DemandEnvelope
    {
        public DmSignsUp(string mailAddress) : base(()=>
            new PoofDemand("user", "registration", "sign-up")
                .Refined("mail", mailAddress)
        )
        { }
    }
}
