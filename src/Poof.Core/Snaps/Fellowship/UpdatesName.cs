using Poof.Core.Entity.Fellowship;
using Poof.Core.Entity.Membership;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.List;

namespace Poof.Core.Snaps.Fellowship
{
    /// <summary>
    /// Updates the name of the given fellowship
    /// </summary>
    public sealed class UpdatesName : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Updates the name of the given fellowship
        /// </summary>
        /// <param name="mem"></param>
        /// <param name="identity"></param>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public UpdatesName(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var fellowship = dmd.Param("fellowship");
            var memberships = new Memberships(mem);
            var existentMembers =
                new Mapped<string, string>(membership =>
                    new Owner.Of(new MembershipOf(mem, membership)).AsString(),
                    memberships.List(new Team.Match(fellowship))
                );
            if (!existentMembers.Contains(identity.UserID()))
            {
                throw new InvalidOperationException($"Unable to update name of fellowship '{fellowship}', " +
                    $"because the requesting user is not a member.");
            }

            var name = dmd.Param("name");
            if (new Fellowships(mem).List(new Name.Match(name)).Count > 0)
            {
                throw new ArgumentException($"Unable to update name of fellowship, because the name '{name}' is already taken.");
            }

            new FellowshipOf(mem, fellowship).Update(
                new Name(name)
            );
        })
        { }
    }
}
