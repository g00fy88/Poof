using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.Fellowship
{
    public sealed class Fellowships : CatalogEnvelope
    {
        public Fellowships(IDataBuilding mem) : base(
            new DataCatalog(()=>mem.Moved("fellowship"))
        )
        { }
    }
}
