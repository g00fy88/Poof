using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Entity
{
    public interface ICatalog
    {
        IList<string> List(params IPropMatch[] matches);

        string New();

        void Put(string id);

        void Remove(string id);
    }
}
