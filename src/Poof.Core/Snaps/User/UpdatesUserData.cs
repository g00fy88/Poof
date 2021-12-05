using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Snaps.User.Facets;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace Poof.Core.Snaps.User.Configuration
{
    /// <summary>
    /// Updates the user data.
    /// The pseudonym name is updated and a unque pseudonym number is evaluated.
    /// </summary>
    public sealed class UpdatesUserData : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Updates the user data.
        /// The pseudonym name is updated and a unque pseudonym number is evaluated.
        /// </summary>
        public UpdatesUserData(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var pseudoname = dmd.Param("pseudonym");
            var pseudonumber = new NewPseudonumber(mem, pseudoname).Value();

            new UserOf(mem, identity.UserID()).Update(
                new Pseudonym(pseudoname, pseudonumber)
            );

            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
