using System;
using System.Collections.Generic;
using System.Text;
using Poof.Snaps;
using Poof.Snaps.Demand;
using Yaapii.Atoms;

namespace Poof.Talk.Snaps
{
    public sealed class PoofDemand : DemandEnvelope
    {
        public PoofDemand(string entity, string category, string action) : base(
            new EmptyDemand()
            .Refined("entity", entity)
            .Refined("category", category)
            .Refined("action", action)
        )
        { }

        public PoofDemand(string entity, string category, string action, IInput body) : base(()=>
           new DemandOf(body)
            .Refined("entity", entity)
            .Refined("category", category)
            .Refined("action", action)
        )
        { }
    }
}
