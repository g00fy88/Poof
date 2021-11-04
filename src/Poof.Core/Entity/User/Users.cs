using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.User
{
    public sealed class Users : CatalogEnvelope    {
        public Users(IDataBuilding mem) : base(
            new DataCatalog(()=>mem.Moved("user"))
        )
        { }
    }
}
