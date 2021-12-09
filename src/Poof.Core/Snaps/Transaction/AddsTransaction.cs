using Poof.Core.Entity.Transaction;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Pulse;
using Poof.Snaps;
using Pulse;
using System;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Transaction
{
    /// <summary>
    /// Adds a transaction, adds the given amount to the points
    /// of the giveside and substracts it from the take side (identity).
    /// Also adds the balance score to the give side
    /// </summary>
    public sealed class AddsTransaction : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Adds a transaction, adds the given amount to the points
        /// of the giveside and substracts it from the take side (identity).
        /// Also adds the balance score to the give side
        /// </summary>
        public AddsTransaction(IDataBuilding mem, IPulse pulse, IIdentity identity) : base(dmd =>
        {
            var json = new JSONOf(dmd.Body());
            var receiverType = new Strict(json.Value("givetype"), "user", "fellowship").AsString();
            var receiverId = json.Value("giveside");
            var senderType = new Strict(json.Value("taketype"), "user", "fellowship").AsString();
            var senderId = json.Value("takeside");
            if(senderType == "user")
            {
                senderId = identity.UserID();
            }
            var amount = new DoubleOf(json.Value("amount")).Value();

            if(amount < 0)
            {
                throw new ArgumentException("Unable to add transaction. Negative amounts are not allowed.");
            }

            new TransactionOf(mem, new Transactions(mem).New()).Update(
                new Title(json.Value("title")),
                new TakeSide(senderType, senderId),
                new GiveSide(receiverType, receiverId),
                new Date(DateTime.Now),
                new Amount(amount)
            );

            var spender = new UserOf(mem, identity.UserID());
            var receiver = new UserOf(mem, receiverId);
            var balanceFactor = new Points.GiveFactor(receiver).Value() + new Points.TakeFactor(spender).Value();
            receiver.Update(
                new Points(amount),
                new BalanceScore(amount * balanceFactor)
            );
            try
            {
                spender.Update(
                    new Points(-amount)
                );
                pulse.Send(new SigStatusChanged(receiverId, "transaction"));
            }
            catch(Exception)
            {
                receiver.Update(
                    new Points(-amount),
                    new BalanceScore(-amount * balanceFactor)
                );
                throw;
            }
        })
        { }
    }
}
