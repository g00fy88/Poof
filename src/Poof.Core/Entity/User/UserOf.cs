using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.User
{
    public sealed class UserOf : EntityEnvelope
    {
        public UserOf(IDataBuilding mem, string id) : base(
            new EntityOf(() =>
                mem.Moved("user").Floor(id)
            )
        )
        { }
    }
}
