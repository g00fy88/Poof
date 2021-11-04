using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.Transaction
{
    public sealed class TransactionOf : EntityEnvelope
    {
        public TransactionOf(IDataBuilding mem, string id) : base(
            new EntityOf(() =>
                mem.Moved("transaction").Floor(id)
            )
        )
        { }
    }
}
