using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Entity
{
    public interface IEntityInput
    {
        IDataFloor Apply(IDataFloor mem);
    }
}
