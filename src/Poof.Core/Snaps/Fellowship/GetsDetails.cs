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
    /// The details and members of the given fellowship
    /// </summary>
    public sealed class GetsDetails : SnapEnvelope<IInput>
    {
        /// <summary>
        /// The details and members of the given fellowship
        /// </summary>
        public GetsDetails(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var fellowshipId = dmd.Param("fellowship");

            if(new Memberships(mem).List(new Team.Match(fellowshipId), new Owner.Match(identity.UserID())).Count == 0)
            {
                throw new InvalidOperationException($"Unable to retrieve details of fellowship '{fellowshipId}', " +
                            $"because the requesting user is not a member.");
            }

            var fellowship = new FellowshipOf(mem, fellowshipId);
            return
                new JsonRawOutcome(
                    new JObject(
                        new JProperty("name", new Name.Of(fellowship).AsString()),
                        new JProperty("pictureUrl", new Picture.Base64Url(fellowship).AsString()),
                        new JProperty("score", new TextOf(new Score.Activity(mem, fellowshipId).Value()).AsString()),
                        new JProperty("givefactor", new TextOf(new Factor.Give(mem, fellowshipId).Value()).AsString()),
                        new JProperty("takefactor", new TextOf(new Factor.Take(mem, fellowshipId).Value()).AsString()),
                        new JProperty("members",
                            new JArray(
                                new Mapped<string, JObject>(membership =>
                                {
                                    var userId = new Owner.Of(new MembershipOf(mem, membership)).AsString();
                                    var user = new UserOf(mem, userId);
                                    return
                                        new JObject(
                                            new JProperty("id", userId),
                                            new JProperty("pseudonym", new Pseudonym.Name(user).AsString()),
                                            new JProperty("pseudonumber", new Pseudonym.Number(user).Value()),
                                            new JProperty("pictureUrl", new Picture.Base64Url(user).AsString()),
                                            new JProperty("score", new TextOf(new BalanceScore.Total(user).Value()).AsString()),
                                            new JProperty("givefactor", new TextOf(new Points.GiveFactor(user).Value()).AsString()),
                                            new JProperty("takefactor", new TextOf(new Points.TakeFactor(user).Value()).AsString())
                                        );
                                }, new Memberships(mem).List(new Team.Match(fellowshipId))
                                )
                            )
                        )
                    )
                );
        })
        { }
    }
}
