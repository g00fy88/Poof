using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Snaps.Fellowship
{
    public sealed class DmRemoveMembership : DemandEnvelope
    {
        public DmRemoveMembership(string fellowship) : base(()=>
            new PoofDemand("fellowship", "configuration", "remove-membership")
                .Refined("team", fellowship)
        )
        { }
    }
}
