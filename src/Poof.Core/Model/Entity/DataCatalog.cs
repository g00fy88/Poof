using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Entity
{
    public sealed class DataCatalog : ICatalog
    {
        private readonly Func<IDataBuilding> memory;

        public DataCatalog(Func<IDataBuilding> memory)
        {
            this.memory = memory;
        }

        public IList<string> List(params IPropMatch[] matches)
        {
            return this.memory().Floors(matches);
        }

        public string New()
        {
            var id = Guid.NewGuid().ToString();
            Put(id);
            return id;
        }

        public void Put(string id)
        {
            this.memory().Add(id);
        }

        public void Remove(string id)
        {
            this.memory().Remove(id);
        }
    }
}
