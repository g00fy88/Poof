using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Entity
{
    public interface IEntity
    {
        public void Update(params IEntityInput[] inputs);

        public void Update(IEnumerable<IEntityInput> inputs);

        IDataFloor Memory();
    }
}
