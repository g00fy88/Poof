using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Scalar;

namespace Poof.Core.Model.Entity
{
    public sealed class EntityOf : IEntity
    {
        private readonly IScalar<IDataFloor> memory;

        public EntityOf(Func<IDataFloor> memory) : this(
            new ScalarOf<IDataFloor>(memory)
        )
        { }

        public EntityOf(IScalar<IDataFloor> memory)
        {
            this.memory = memory;
        }

        public IDataFloor Memory()
        {
            return this.memory.Value();
        }

        public void Update(params IEntityInput[] inputs)
        {
            Update(new ManyOf<IEntityInput>(inputs));
        }

        public void Update(IEnumerable<IEntityInput> inputs)
        {
            var mem = this.memory.Value();
            foreach (var input in inputs)
            {
                mem = input.Apply(mem);
            }
        }
    }
}
