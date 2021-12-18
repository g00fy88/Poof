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

namespace Poof.Core.Snaps.Fellowship
{
    /// <summary>
    /// Adds a new member to the given fellowship.
    /// Works only, if the requesting user is a member of the fellowship itself.
    /// </summary>
    public sealed class AddsMembership : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Adds a new member to the given fellowship.
        /// Works only, if the requesting user is a member of the fellowship itself.
        /// </summary>
        public AddsMembership(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var fellowship = dmd.Param("team");
            var member = dmd.Param("newmember");
            var memberships = new Memberships(mem);
            var existentMembers =
                new Mapped<string, string>(membership =>
                    new Owner.Of(new MembershipOf(mem, membership)).AsString(),
                    memberships.List(new Team.Match(fellowship))
                );
            if(!existentMembers.Contains(identity.UserID()))
            {
                throw new InvalidOperationException($"Unable to add new member to fellowship '{fellowship}', " +
                    $"because the requesting user is not a member.");
            }
            if(existentMembers.Contains(member))
            {
                throw new InvalidOperationException($"Unable to add new member '{member}' to fellowship '{fellowship}', " +
                    $"because a membership already exists.");
            }

            new MembershipOf(mem, memberships.New()).Update(
                new Team(fellowship),
                new Owner(member),
                new Share(1)
            );
        })
        { }
    }
}
