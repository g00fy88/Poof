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

namespace Poof.Core.Snaps.User
{
    /// <summary>
    /// Gets the nearby users to the given location
    /// </summary>
    public sealed class GetsNearbyUsers : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Gets the nearby users to the given location
        /// </summary>
        public GetsNearbyUsers(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
             var users = new List<JObject>();
            foreach(var id in new Users(mem).List())
            {
                var user = new UserOf(mem, id);
                users.Add(
                    new JObject(
                        new JProperty("id", id),
                        new JProperty("mail", new Mail.Of(user).AsString()),
                        new JProperty("points", new TextOf(new Points.Of(user).Value()).AsString()),
                        new JProperty("me", identity.UserID().Equals(id))
                    )
                );
            }

            return new JsonRawOutcome(new JArray(users));
        })
        { }
    }
}
