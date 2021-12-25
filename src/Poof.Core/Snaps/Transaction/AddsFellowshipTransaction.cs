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
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Number;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Transaction
{
    /// <summary>
    /// Adds a transaction, adds the given amount to the points
    /// of the giveside and substracts it from the take side (identity).
    /// Also adds the balance score to the give side
    /// </summary>
    public sealed class AddsFellowshipTransaction : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Adds a transaction, adds the given amount to the points
        /// of the giveside and substracts it from the take side (identity).
        /// Also adds the balance score to the give side
        /// </summary>
        public AddsFellowshipTransaction(IDataBuilding mem, IPulse pulse, IIdentity identity) : base(dmd =>
        {
            var json = new JSONOf(dmd.Body());
            var receiverType = new Strict(json.Value("givetype"), "user", "fellowship").AsString();
            var receiverId = json.Value("giveside");

            var senderId = json.Value("takeside");

            var amount = new DoubleOf(json.Value("amount")).Value();

            if(amount < 0)
            {
                throw new ArgumentException("Unable to add transaction. Negative amounts are not allowed.");
            }

            new TransactionOf(mem, new Transactions(mem).New()).Update(
                new Title(json.Value("title")),
                new Entity.Type("main"),
                new TakeSide("fellowship", senderId),
                new GiveSide(receiverType, receiverId),
                new Date(DateTime.Now),
                new Amount(amount)
            );


            var memberships = new Memberships(mem).List(new Team.Match(senderId));
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
                    new TakeSide("user", userId),
                    new GiveSide("fellowship", senderId),
                    new Date(DateTime.Now),
                    new Amount(amount)
                );

                user.Update(
                    new Points(-sharedAmount)
                );
                pulse.Send(new SigStatusChanged(userId, "transaction"));
            }
        })
        { }
    }
}
