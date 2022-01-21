using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.IO;

namespace Poof.Talk.Snaps.User.Configuration
{
    public sealed class DmUpdatePicture : DemandEnvelope
    {
        public DmUpdatePicture(byte[] picture) : this(
            new InputOf(picture)
        )
        { }

        public DmUpdatePicture(IInput picture) : base(()=>
            new PoofDemand("user", "configuration", "update-picture", picture)
        )
        { }
    }
}
