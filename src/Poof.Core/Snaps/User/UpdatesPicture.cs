using Poof.Core.Entity;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;

namespace Poof.Core.Snaps.User
{
    public sealed  class UpdatesPicture : SnapEnvelope<IInput>
    {
        public UpdatesPicture(IDataBuilding mem, IIdentity identity) : base(dmd =>
            new UserOf(mem, identity.UserID()).Update(
                new Picture(dmd.Body())
            )
        )
        { }
    }
}
