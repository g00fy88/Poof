using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Snaps.Fellowship;
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
                new FwEntity("fellowship",
                    new FwCategory("configuration",
                        new FwAction("add-fellowship", new AddsFellowship(mem, identity)),
                        new FwAction("add-membership", new AddsMembership(mem, identity)),
                        new FwAction("remove-membership", new RemovesMembership(mem, identity)),
                        new FwAction("update-name", new UpdatesName(mem, identity))
                    ),
                    new FwCategory("discovery",
                        new FwAction("check-name-availability", new CheckNameAvailability(mem))
                    )
                ),
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
                        new FwAction("update-user", new UpdatesUserData(mem, identity)),
                        new FwAction("update-picture", new UpdatesPicture(mem, identity)),
                        new FwAction("add-friend", new AddsFriend(mem, identity)),
                        new FwAction("remove-friend", new RemovesFriend(mem, identity))
                    ),
                    new FwCategory("discovery",
                        new FwAction("get-nearby-users", new GetsNearbyUsers(mem, identity)),
                        new FwAction("find-by-name", new FindsByName(mem, identity)),
                        new FwAction("get-details", new GetsDetails(mem, identity)),
                        new FwAction("get-friends", new GetsFriends(mem, identity))
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
