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
    public sealed class DmUpdatePicture : DemandEnvelope
    {
        public DmUpdatePicture(
            string quest,
            IInput picture
        ) : base(()=>
            new PoofDemand("quest", "configuration", "update-picture",
                picture
            ).Refined("quest", quest)
        )
        { }
    }
}
