using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

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
