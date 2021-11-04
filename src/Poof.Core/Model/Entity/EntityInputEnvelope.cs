using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Entity
{
    public abstract class EntityInputEnvelope : IEntityInput
    {
        private readonly Action<IDataFloor> apply;

        public EntityInputEnvelope(Action<IDataFloor> apply)
        {
            this.apply = apply;
        }

        public IDataFloor Apply(IDataFloor mem)
        {
            this.apply(mem);
            return mem;
        }
    }
}
