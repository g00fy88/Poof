using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Snaps.Fellowship
{
    public sealed class DmGetCatalog : DemandEnvelope
    {
        public DmGetCatalog() : base(
            new PoofDemand("fellowship", "discovery", "get-catalog")
        )
        { }
    }
}
