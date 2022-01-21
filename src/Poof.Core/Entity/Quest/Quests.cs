using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;

namespace Poof.Core.Entity.Quest
{
    public sealed class Quests : CatalogEnvelope    {
        public Quests(IDataBuilding mem) : base(
            new DataCatalog(() => mem.Moved("quest"))
        )
        { }
    }
}
