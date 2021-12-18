using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Snaps.Fellowship
{
    public sealed class DmAddMembership : DemandEnvelope
    {
        public DmAddMembership(string fellowship, string newMember) : base(()=>
            new PoofDemand("fellowship", "configuration", "add-membership")
                .Refined("team", fellowship)
                .Refined("newmember", newMember)
        )
        { }
    }
}
