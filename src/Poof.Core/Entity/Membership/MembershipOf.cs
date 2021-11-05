using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.Membership
{
    public sealed class MembershipOf : EntityEnvelope
    {
        public MembershipOf(IDataBuilding mem, string id) : base(
            new EntityOf(() => 
                mem.Moved("membership").Floor(id)
            )
        )
        { }
    }
}
