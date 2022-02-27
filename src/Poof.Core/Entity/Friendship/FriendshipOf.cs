using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.Friendship
{
    public sealed class FriendshipOf : EntityEnvelope
    {
        public FriendshipOf(IDataBuilding mem, string id) : base(
            new EntityOf(() => 
                mem.Moved("friendship").Floor(id)
            )
        )
        { }
    }
}
