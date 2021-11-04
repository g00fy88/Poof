using System;
using System.Collections.Generic;
using System.Text;
using Poof.Snaps;
using Poof.Snaps.Flow;
using Yaapii.Atoms;

namespace Poof.Core.Snaps
{
    public sealed class FwCategory : FlowEnvelope<IInput>
    {
        public FwCategory(string category, params IFlow<IInput>[] next) : base(
            new FwParam<IInput>("category", category, next)
        )
        { }
    }
}
