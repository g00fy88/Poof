using Poof.Snaps.Demand;

namespace Poof.Talk.Snaps.User.Discovery
{
    public sealed class DmFindByName : DemandEnvelope
    {
        public DmFindByName(string name) : base(()=>
            new PoofDemand("user", "discovery", "find-by-name")
                .Refined("name", name)
        )
        { }
    }
}
