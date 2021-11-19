using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Snaps.Transaction;
using Poof.Core.Snaps.User;
using Poof.Core.Snaps.User.Configuration;
using Poof.Snaps;
using Poof.Snaps.Flow;
using Pulse;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;

namespace Poof.Core.Snaps
{
    public sealed class PrivateSnap : ISnap<IInput>
    {
        private readonly IFlow<IInput> flow;

        public PrivateSnap(IDataBuilding mem, IPulse pulse, IIdentity identity) : this(
            new FwChain<IInput>(
                new FwEntity("transaction",
                    new FwCategory("configuration",
                        new FwAction("add-transaction", new AddsTransaction(mem, pulse, identity))
                        
                    ),
                    new FwCategory("discovery",
                        new FwAction("get-user-transactions", new GetsUserTransactions(mem, identity))
                    )
                ),
                new FwEntity("user",
                    new FwCategory("configuration",
                        new FwAction("update-user", new UpdatesUserData(mem, identity))
                    ),
                    new FwCategory("discovery",
                        new FwAction("get-nearby-users", new GetsNearbyUsers(mem, identity)),
                        new FwAction("find-by-name", new FindsByName(mem, identity)),
                        new FwAction("get-details", new GetsDetails(mem, identity))
                    )
                )
            )
        )
        { }

        public PrivateSnap(IFlow<IInput> flow)
        {
            this.flow = flow;
        }

        public IOutcome<IInput> Convert(IDemand demand)
        {
            var response = this.flow.Response(demand);
            if(!response.Has())
            {
                throw new InvalidOperationException(
                    new Paragraph($"Unable to process public request with parameters",
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
