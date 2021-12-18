using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Snaps.Fellowship
{
    public sealed class DmAddFellowship : DemandEnvelope
    {
        public DmAddFellowship(string name) : base(()=>
            new PoofDemand("fellowship", "configuration", "add-fellowship")
                .Refined("name", name)
        )
        { }
    }
}
