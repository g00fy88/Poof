using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.Quest
{
    public sealed class QuestOf : EntityEnvelope
    {
        public QuestOf(IDataBuilding mem, string id) : base(
            new EntityOf(() =>
                mem.Moved("quest").Floor(id)
            )
        )
        { }
    }
}
