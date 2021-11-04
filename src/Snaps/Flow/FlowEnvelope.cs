using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Snaps.Flow
{
    public abstract class FlowEnvelope<TResult> : IFlow<TResult>
    {
        private readonly IFlow<TResult> flow;

        public FlowEnvelope(IFlow<TResult> flow)
        {
            this.flow = flow;
        }

        public IOpt<TResult> Response(IDemand demand)
        {
            return this.flow.Response(demand);
        }
    }
}
