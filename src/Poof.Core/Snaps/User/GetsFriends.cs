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
using Poof.Core.Entity;

namespace Poof.Core.Snaps.User
{
    /// <summary>
    /// Gets a list of all users with a matching pseudonym and all fellowships with a matching name
    /// </summary>
    public sealed class GetsFriends : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Gets a list of all users with a matching pseudonym and all fellowships with a matching name
        /// </summary>
        public GetsFriends(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var friends = new List<JObject>();

            foreach (var id in new Friends.Of(new UserOf(mem, identity.UserID())))
            {
                var user = new UserOf(mem, id);
                friends.Add(
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("type", "user"),
                        new JProperty("pseudonym", new Pseudonym.Name(user).AsString()),
                        new JProperty("pseudonumber", new Pseudonym.Number(user).Value()),
                        new JProperty("pictureUrl", new Picture.Base64Url(user).AsString()),
                        new JProperty("score", new TextOf(new BalanceScore.Total(user).Value()).AsString()),
                        new JProperty("givefactor", new Points.GiveFactor(user).Value()),
                        new JProperty("takefactor", new Points.TakeFactor(user).Value())
                    )
                );
            }

            return new JsonRawOutcome(new JArray(friends));
        })
        { }
    }
}
