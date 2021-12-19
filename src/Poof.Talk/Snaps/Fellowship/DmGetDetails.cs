using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Snaps.Fellowship
{
    public sealed class DmGetDetails : DemandEnvelope
    {
        public DmGetDetails(string fellowship) : base(
            new PoofDemand("fellowship", "discovery", "get-details")
                .Refined("fellowship", fellowship)
        )
        { }
    }
}
