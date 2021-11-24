using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using Yaapii.Atoms;
using Poof.Core.Model.Data;
using Yaapii.Atoms.Text;
using Yaapii.Atoms.Enumerable;
using Poof.Core.Entity.Fellowship;
using Poof.Core.Entity.Transaction;

namespace Poof.Core.Snaps.User
{
    /// <summary>
    /// The details of the user in the identity
    /// </summary>
    public sealed class GetsDetails : SnapEnvelope<IInput>
    {
        /// <summary>
        /// The details of the user in the identity
        /// </summary>
        public GetsDetails(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var user = new UserOf(mem, identity.UserID());

            return
                new JsonRawOutcome(
                    new JObject(
                        new JProperty("pseudonym",
                            new JObject(
                                new JProperty("name", new Pseudonym.Name(user).AsString()),
                                new JProperty("number", new Pseudonym.Number(user).Value())
                            )
                        ),
                        new JProperty("picture", new Picture.Base64Url(user).AsString()),
                        new JProperty("points", new Points.Of(user).Value()),
                        new JProperty("takeFactor", new Points.TakeFactor(user).Value()),
                        new JProperty("giveFactor", new Points.GiveFactor(user).Value()),
                        new JProperty("score", new BalanceScore.Total(user).Value())
                    )
                );
        })
        { }
    }
}
