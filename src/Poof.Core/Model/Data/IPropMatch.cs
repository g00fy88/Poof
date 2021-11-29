using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Data
{
    public interface IPropMatch
    {
        string Name();
        string Type();
        T Value<T>();
    }
}
