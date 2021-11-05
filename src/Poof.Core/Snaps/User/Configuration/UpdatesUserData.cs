using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;

namespace Poof.Core.Snaps.User.Configuration
{
    public sealed class UpdatesUserData : SnapEnvelope<IInput>
    {
        public UpdatesUserData(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            new UserOf(mem, identity.UserID()).Update(
                new Pseudonym(dmd.Param("pseudonym"))
            );

            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
