using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Snaps.Fellowship
{
    public sealed class DmUpdateName : DemandEnvelope
    {
        public DmUpdateName(string fellowship, string name) : base(()=>
            new PoofDemand("fellowship", "configuration", "update-name")
                .Refined("fellowship", fellowship)
                .Refined("name", name)
        )
        { }
    }
}
