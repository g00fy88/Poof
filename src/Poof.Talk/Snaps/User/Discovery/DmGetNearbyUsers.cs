using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Snaps.User.Discovery
{
    public sealed class DmGetNearbyUsers : DemandEnvelope
    {
        public DmGetNearbyUsers() : base(
            new PoofDemand("user", "discovery", "get-nearby-users")
        )
        { }
    }
}
