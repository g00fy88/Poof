using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Poof.Talk.Snaps;
using System;
using System.Globalization;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Demand.Snaps.Quest
{
    public sealed class DmGetPicture : DemandEnvelope
    {
        public DmGetPicture(
            string quest
        ) : base(()=>
            new PoofDemand("quest", "discovery", "get-picture").Refined("quest", quest)
        )
        { }
    }
}
