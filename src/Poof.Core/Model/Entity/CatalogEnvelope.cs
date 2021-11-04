using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Entity
{
    public abstract class CatalogEnvelope : ICatalog
    {
        private readonly ICatalog catalog;

        public CatalogEnvelope(ICatalog catalog)
        {
            this.catalog = catalog;
        }

        public IList<string> List(params IPropMatch[] matches)
        {
            return this.catalog.List(matches);
        }

        public string New()
        {
            return this.catalog.New();
        }

        public void Put(string id)
        {
            this.catalog.Put(id);
        }

        public void Remove(string id)
        {
            this.catalog.Remove(id);
        }
    }
}
