using Newtonsoft.Json.Linq;
using Poof.Core.Entity.Transaction;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Transaction
{
    /// <summary>
    /// Returns a list of the identity user's transactions with all the details
    /// </summary>
    public sealed class GetsUserTransactions : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Returns a list of the identity user's transactions with all the details
        /// </summary>
        public GetsUserTransactions(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var userId = identity.UserID();
            var userTransactions = 
                new Transactions(mem).List(
                    new Participant.Match(userId), 
                    new Date.SortMatch()
                );
            var result = new JArray();

            foreach(var id in userTransactions)
            {
                var transaction = new TransactionOf(mem, id);
                var giveSide = new GiveSide.Entity(transaction).AsString();
                var takeSide = new TakeSide.Entity(transaction).AsString();
                result.Add(
                    new JObject(
                        new JProperty("title", new Title.Of(transaction).AsString()),
                        new JProperty("date", new Date.Of(transaction).Value().ToString("dd/MM/yyyy H:mm:ss zzz")),
                        new JProperty("amount", new Amount.Of(transaction).Value()),
                        new JProperty("type", giveSide == userId ? "give" : "receive"),
                        new JProperty(giveSide == userId ? "me" : "other",
                            new JObject(
                                new JProperty("name", new Pseudonym.Name(new UserOf(mem, giveSide)).AsString()),
                                new JProperty("score", new BalanceScore.Total(new UserOf(mem, giveSide)).Value()),
                                new JProperty("pictureUrl", new Picture.Base64Url(new UserOf(mem, giveSide)).AsString())
                            )
                        ),
                        new JProperty(takeSide == userId ? "me" : "other",
                            new JObject(
                                new JProperty("name", new Pseudonym.Name(new UserOf(mem, takeSide)).AsString()),
                                new JProperty("score", new BalanceScore.Total(new UserOf(mem, takeSide)).Value()),
                                new JProperty("pictureUrl", new Picture.Base64Url(new UserOf(mem, takeSide)).AsString())
                            )
                        )
                    )
                );
            }

            return new JsonRawOutcome(new JSONOf(result));
        })
        { }
    }
}
