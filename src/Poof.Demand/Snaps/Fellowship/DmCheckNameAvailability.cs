using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Snaps.Fellowship
{
    public sealed class DmCheckNameAvailability : DemandEnvelope
    {
        public DmCheckNameAvailability(string name) : base(()=>
            new PoofDemand("fellowship", "discovery", "check-name-availability")
                .Refined("name", name)
        )
        { }
    }
}
