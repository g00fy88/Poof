using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Entity
{
    public abstract class EntityEnvelope : IEntity
    {
        private readonly IEntity entity;

        public EntityEnvelope(IEntity entity)
        {
            this.entity = entity;
        }

        public IDataFloor Memory()
        {
            return this.entity.Memory();
        }

        public void Update(params IEntityInput[] inputs)
        {
            this.entity.Update(inputs);
        }

        public void Update(IEnumerable<IEntityInput> inputs)
        {
            this.entity.Update(inputs);
        }
    }
}
