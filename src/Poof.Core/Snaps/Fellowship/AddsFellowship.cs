using Poof.Core.Entity.Fellowship;
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

namespace Poof.Core.Snaps.Fellowship
{
    /// <summary>
    /// Adds a new fellowship with the given name and the
    /// identity user as first member
    /// </summary>
    public sealed class AddsFellowship : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Adds a new fellowship with the given name and the
        /// identity user as first member
        /// </summary>
        public AddsFellowship(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var name = dmd.Param("name");
            var fellowships = new Fellowships(mem);
            if (fellowships.List(new Name.Match(name)).Count > 0)
            {
                throw new ArgumentException($"Unable to add new fellowship, because the name '{name}' is already taken.");
            }

            var fellowship = fellowships.New();
            new FellowshipOf(mem, fellowship).Update(
                new Name(name)
            );

            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Owner(identity.UserID()),
                new Team(fellowship),
                new Share(1),
                new Role("admin")
            );
        })
        { }
    }
}
