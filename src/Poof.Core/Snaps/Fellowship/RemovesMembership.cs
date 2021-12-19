using Poof.Core.Entity.Membership;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;

namespace Poof.Core.Snaps.Fellowship
{
    /// <summary>
    /// removes the membership of the requesting user from the given fellowship.
    /// Works only, if the requesting user is a member of the fellowship itself.
    /// </summary>
    public sealed class RemovesMembership : SnapEnvelope<IInput>
    {
        /// <summary>
        /// removes the membership of the requesting user from the given fellowship.
        /// Works only, if the requesting user is a member of the fellowship itself.
        /// </summary>
        public RemovesMembership(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var fellowship = dmd.Param("team");
            var memberships = new Memberships(mem);

            memberships.Remove(
                new FirstOf<string>(
                    memberships.List(new Team.Match(fellowship), new Owner.Match(identity.UserID())),
                    new InvalidOperationException($"Unable to remove the membership of fellowship '{fellowship}', " +
                        $"because the requesting user is not a member.")
                ).Value()
            );
        })
        { }
    }
}
