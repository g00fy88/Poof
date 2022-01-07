using Newtonsoft.Json.Linq;
using Poof.Core.Entity;
using Poof.Core.Entity.Fellowship;
using Poof.Core.Entity.Membership;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;

namespace Poof.Core.Snaps.Fellowship
{
    /// <summary>
    /// List of all fellowships, where the requesting user is a member in
    /// </summary>
    public sealed class GetsCatalog : SnapEnvelope<IInput>
    {
        /// <summary>
        /// List of all fellowships, where the requesting user is a member in
        /// </summary>
        public GetsCatalog(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var memberships = new Memberships(mem);
            var result = new List<JObject>();
            foreach (var membership in memberships.List(new Owner.Match(identity.UserID())))
            {
                var fellowshipId = new Team.Of(new MembershipOf(mem, membership)).AsString();
                var fellowship = new FellowshipOf(mem, fellowshipId);
                result.Add(
                    new JObject(
                        new JProperty("id", fellowshipId),
                        new JProperty("name", new Name.Of(fellowship).AsString()),
                        new JProperty("pictureUrl", new Picture.Base64Url(fellowship).AsString()),
                        new JProperty("level", new TextOf(new Score.ActivityLevel(mem, fellowshipId).Value()).AsString()),
                        new JProperty("givefactor", new TextOf(new Factor.Give(mem, fellowshipId).Value()).AsString()),
                        new JProperty("takefactor", new TextOf(new Factor.Take(mem, fellowshipId).Value()).AsString())
                    )
                );
            }

            return new JsonRawOutcome(new JArray(result));
        })
        {

        }
    }
}
