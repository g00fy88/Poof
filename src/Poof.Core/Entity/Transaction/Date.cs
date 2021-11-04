using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model.Entity;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Transaction
{
    /// <summary>
    /// The date of the transaction
    /// </summary>
    public sealed class Date : EntityInputEnvelope
    {
        /// <summary>
        /// The date of the transaction
        /// </summary>
        public Date(DateTime date) : base(mem =>
            mem.Update("date", date)
        )
        { }

        /// <summary>
        /// The date of the transaction
        /// </summary>
        public sealed class Of : ScalarEnvelope<DateTime>
        {
            /// <summary>
            /// The date of the transaction
            /// </summary>
            public Of(IEntity transaction) : base(()=>
                transaction.Memory().Prop<DateTime>("date")
            )
            { }
        }
    }
}
