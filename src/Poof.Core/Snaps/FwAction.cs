using System;
using System.Collections.Generic;
using System.Text;
using Poof.Snaps;
using Poof.Snaps.Flow;
using Yaapii.Atoms;

namespace Poof.Core.Snaps
{
    public sealed class FwAction : FlowEnvelope<IInput>
    {
        public FwAction(string action, ISnap<IInput> next) : this(
            action,
            new FwResponse<IInput>(next)
        )
        { }

        public FwAction(string action, params IFlow<IInput>[] next) : base(
            new FwParam<IInput>("action", action, next)
        )
        { }
    }
}
