using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Future;
using Poof.Core.Snaps.Fellowship;
using Poof.Core.Snaps.Quest;
using Poof.Core.Snaps.Transaction;
using Poof.Core.Snaps.Transaction.Facets;
using Poof.Core.Snaps.User;
using Poof.Core.Snaps.User.Configuration;
using Poof.Snaps;
using Poof.Snaps.Flow;
using Pulse;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;

namespace Poof.Core.Snaps
{
    public sealed class FutureSnap : ISnap<IInput>
    {
        private readonly IFlow<IInput> flow;

        public FutureSnap(IDataBuilding mem, IPulse pulse, IFuture future) : this(
            new FwChain<IInput>(
                new FwEntity("quest",
                    new FwCategory("configuration",
                        new FwAction("add-weeklies", new AddsWeeklyQuests(mem, future)),
                        new FwAction("fail-unfinished", new FailsWhenUnfinished(mem, future))
                    )
                )
            )
        )
        { }

        public FutureSnap(IFlow<IInput> flow)
        {
            this.flow = flow;
        }

        public IOutcome<IInput> Convert(IDemand demand)
        {
            var response = this.flow.Response(demand);
            if(!response.Has())
            {
                throw new InvalidOperationException(
                    new Paragraph($"Unable to process future request with parameters",
                        $"entity: {demand.Param("entity")}",
                        $"category: {demand.Param("category")}",
                        $"action: {demand.Param("action")}.",
                        "No valid match was found."
                    ).AsString()
                );
            }

            return response.Value();
        }
    }
}
