using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity
{
    /// <summary>
    /// The Title of the transaction
    /// </summary>
    public sealed class Type : EntityInputEnvelope
    {
        /// <summary>
        /// The Title of the transaction
        /// </summary>
        public Type(string type) : base(floor =>
            floor.Update("type", type)
        )
        { }

        /// <summary>
        /// The Title of the transaction
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The Title of the transaction
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
