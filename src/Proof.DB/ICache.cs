using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poof.DB
{
    public interface ICache<T>
    {
        T Value(string id);
        void Clear();
    }
}
