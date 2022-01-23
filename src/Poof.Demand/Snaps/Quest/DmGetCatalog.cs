using Poof.Snaps.Demand;
using Poof.Talk.Snaps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Demand.Snaps.Quest
{
    public sealed class DmGetCatalog : DemandEnvelope
    {
        public DmGetCatalog() : base(
            new PoofDemand("quest", "discovery", "get-catalog")
        )
        { }
    }
}
