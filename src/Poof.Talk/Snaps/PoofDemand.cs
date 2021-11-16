using Poof.Snaps;
using Poof.Snaps.Demand;
using Yaapii.Atoms;
using Yaapii.JSON;

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

        public PoofDemand(string entity, string category, string action, IJSON body) : base(() =>
           new DemandOf(body)
            .Refined("entity", entity)
            .Refined("category", category)
            .Refined("action", action)
        )
        { }
    }
}
