using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Data
{
    public interface IDataFloor
    {
        T Prop<T>(string name);

        void Update<T>(string name, T value);
    }
}
