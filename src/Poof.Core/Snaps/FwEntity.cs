using System;
using System.Collections.Generic;
using System.Text;
using Poof.Snaps;
using Poof.Snaps.Flow;
using Yaapii.Atoms;

namespace Poof.Core.Snaps
{
    public sealed class FwEntity : FlowEnvelope<IInput>
    {
        public FwEntity(string entity, params IFlow<IInput>[] next) : base(
            new FwParam<IInput>("entity", entity, next)
        )
        { }
    }
}
