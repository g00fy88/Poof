using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Data
{
    public interface IDataBuilding
    {
        IList<string> Floors(params IPropMatch[] matches);
        
        IDataFloor Floor(string id);

        void Add(string floor);

        void Remove(string floor);

        IDataBuilding Moved(string name);
    }
}
