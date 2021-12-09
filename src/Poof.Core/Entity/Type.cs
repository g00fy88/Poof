using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity
{
    /// <summary>
    /// The type of the entity
    /// </summary>
    public sealed class Type : EntityInputEnvelope
    {
        /// <summary>
        /// The type of the entity
        /// </summary>
        public Type(string type) : base(floor =>
            floor.Update("type", type)
        )
        { }

        /// <summary>
        /// The type of the entity
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The type of the entity
            /// </summary>
            public Of(IEntity entity) : base(()=>
                entity.Memory().Prop<string>("type"),
                false
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string type) : base(
                "type",
                "equals",
                type
            )
            { }
        }
    }
}
