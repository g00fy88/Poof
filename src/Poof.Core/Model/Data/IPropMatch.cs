using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Data
{
    public interface IPropMatch
    {
        string Name();
        bool Allows(string value);
    }
}
