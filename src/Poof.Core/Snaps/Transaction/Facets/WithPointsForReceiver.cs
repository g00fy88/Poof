using Poof.Core.Entity.Fellowship;
using Poof.Core.Entity.Membership;
using Poof.Core.Entity.Transaction;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Pulse;
using Poof.Snaps;
using Pulse;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Number;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Transaction.Facets
{
    /// <summary>
    /// A decorator that adds points to the receiving part of the transaction,
    /// and informs the receivers over broadcast.
    /// If the receiver is a fellowship, automatic transactions from the fellowship to its members with the amount shared are added.
    /// </summary>
    public sealed class WithPointsForReceiver : SnapEnvelope<IInput>
    {
        /// <summary>
        /// A decorator that adds points to the receiving part of the transaction,
        /// and informs the receivers over broadcast.
        /// If the receiver is a fellowship, automatic transactions from the fellowship to its members with the amount shared are added.
        /// </summary>
        public WithPointsForReceiver(IDataBuilding mem, IPulse pulse, IIdentity identity, ISnap<IInput> origin) : base(dmd =>
        {
            origin.Convert(dmd);

            var json = new JSONOf(dmd.Body());
            var amount = new DoubleOf(json.Value("amount")).Value();
            var receiverType = new Strict(json.Value("givetype"), "user", "fellowship").AsString();
            var receiverId = json.Value("giveside");

            var senderType = new Strict(json.Value("taketype"), "user", "fellowship").AsString();
            var senderId =
                new MapOf(
                    "user", identity.UserID(),
                    "fellowship", json.Value("takeside")
                )[senderType];

            var takeFactor =
                new MapOf<double>(
                    new KvpOf<double>("user", () => new Points.TakeFactor(new UserOf(mem, senderId)).Value()),
                    new KvpOf<double>("fellowship", () => new Factor.Take(mem, senderId).Value())
                )[senderType];

            if (receiverType == "user")
            {
                var user = new UserOf(mem, receiverId);
                var balanceFactor = new Points.GiveFactor(user).Value() + takeFactor;

                user.Update(
                    new Points(amount),
                    new BalanceScore(amount * balanceFactor)
                );
                pulse.Send(new SigStatusChanged(receiverId, "transaction"));
            }
            else
            {
                var memberships = new Memberships(mem).List(new Team.Match(receiverId));
                var totalShare =
                    new SumOf(
                        new Mapped<string, double>(id =>
                            new Share.Of(new MembershipOf(mem, id)).Value(),
                            memberships
                        )
                    ).AsDouble();

                foreach (var id in memberships)
                {
                    var membership = new MembershipOf(mem, id);
                    var userId = new Owner.Of(membership).AsString();
                    var user = new UserOf(mem, userId);
                    var sharedAmount = amount * new Share.Of(membership).Value() / totalShare;

                    new TransactionOf(mem, new Transactions(mem).New()).Update(
                        new Title(json.Value("title")),
                        new Entity.Type("automatic"),
                        new TakeSide("fellowship", receiverId),
                        new GiveSide("user", userId),
                        new Date(DateTime.Now),
                        new Amount(amount)
                    );

                    var balanceFactor = new Points.GiveFactor(user).Value() + takeFactor;

                    user.Update(
                        new Points(sharedAmount),
                        new BalanceScore(sharedAmount * balanceFactor)
                    );
                    pulse.Send(new SigStatusChanged(userId, "transaction"));
                }
            }
        })
        { }
    }
}
