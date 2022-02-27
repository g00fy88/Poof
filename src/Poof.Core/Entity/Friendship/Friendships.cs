using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.Friendship
{
    public sealed class Friendships : CatalogEnvelope
    {
        public Friendships(IDataBuilding mem) : base(
            new DataCatalog(()=>mem.Moved("friendship"))
        )
        { }
    }
}
