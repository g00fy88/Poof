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
    public sealed class PrivateSnap : ISnap<IInput>
    {
        private readonly IFlow<IInput> flow;

        public PrivateSnap(IDataBuilding mem, IPulse pulse, IIdentity identity, IFuture future) : this(
            new FwChain<IInput>(
                new FwEntity("fellowship",
                    new FwCategory("configuration",
                        new FwAction("add-fellowship", new AddsFellowship(mem, identity)),
                        new FwAction("add-membership", new AddsMembership(mem, identity)),
                        new FwAction("remove-membership", new RemovesMembership(mem, identity)),
                        new FwAction("update-name", new UpdatesName(mem, identity))
                    ),
                    new FwCategory("discovery",
                        new FwAction("check-name-availability", new CheckNameAvailability(mem)),
                        new FwAction("get-catalog", new Fellowship.GetsCatalog(mem, identity)),
                        new FwAction("get-details", new Fellowship.GetsDetails(mem, identity))
                    )
                ),
                new FwEntity("quest",
                    new FwCategory("configuration",
                        new FwAction("add-applicant", new AddsApplicant(mem, identity, future)),
                        new FwAction("finish-quest", new FinishesQuest(mem, identity)),
                        new FwAction("create-quest", new CreatesQuest(mem, identity, future)),
                        new FwAction("update-picture", new Quest.UpdatesPicture(mem, identity))
                    ),
                    new FwCategory("discovery",
                        new FwAction("get-catalog", new Quest.GetsCatalog(mem, identity)),
                        new FwAction("get-picture", new GetsPicture(mem))
                    )
                ),
                new FwEntity("transaction",
                    new FwCategory("configuration",
                        new FwAction("add-fellowship-transaction",
                            new WithPointsForReceiver(mem, pulse, identity,
                                new AddsFellowshipTransaction(mem, pulse, identity)
                            )
                        ),
                        new FwAction("add-user-transaction", 
                            new WithPointsForReceiver(mem, pulse, identity,
                                new AddsUserTransaction(mem, identity)
                            )
                        )
                    ),
                    new FwCategory("discovery",
                        new FwAction("get-user-transactions", new GetsUserTransactions(mem, identity))
                    )
                ),
                new FwEntity("user",
                    new FwCategory("configuration",
                        new FwAction("update-user", new UpdatesUserData(mem, identity)),
                        new FwAction("update-picture", new User.UpdatesPicture(mem, identity)),
                        new FwAction("add-friend", new AddsFriend(mem, identity)),
                        new FwAction("confirm-friend", new ConfirmsFriend(mem, identity)),
                        new FwAction("remove-friend", new RemovesFriend(mem, identity))
                    ),
                    new FwCategory("discovery",
                        new FwAction("get-nearby-users", new GetsNearbyUsers(mem, identity)),
                        new FwAction("find-by-name", new FindsByName(mem, identity)),
                        new FwAction("get-details", new User.GetsDetails(mem, identity)),
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
                    new Paragraph($"Unable to process private request with parameters",
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
