using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.Membership
{
    public sealed class Memberships : CatalogEnvelope
    {
        public Memberships(IDataBuilding mem) : base(
            new DataCatalog(()=>mem.Moved("membership"))
        )
        { }
    }
}
