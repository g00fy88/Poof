using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Transaction
{
    /// <summary>
    /// The Title of the transaction
    /// </summary>
    public sealed class Initiator : EntityInputEnvelope
    {
        /// <summary>
        /// The Title of the transaction
        /// </summary>
        public Initiator(string user) : base(floor =>
            floor.Update("initiator", user)
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
            public Of(IEntity transaction) : base(()=>
                transaction.Memory().Prop<string>("initiator"),
                false
            )
            { }
        }
    }
}
