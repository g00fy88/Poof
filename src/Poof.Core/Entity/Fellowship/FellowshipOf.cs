using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.Fellowship
{
    public sealed class FellowshipOf : EntityEnvelope
    {
        public FellowshipOf(IDataBuilding mem, string id) : base(
            new EntityOf(() => 
                mem.Moved("fellowship").Floor(id)
            )
        )
        { }
    }
}
