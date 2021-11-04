using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model.Entity;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Transaction
{
    /// <summary>
    /// The amount of points in this transaction
    /// </summary>
    public sealed class Amount : EntityInputEnvelope
    {
        /// <summary>
        /// The amount of points in this transaction
        /// </summary>
        public Amount(double points) : base(mem =>
            mem.Update("amount", points)
        )
        { }

        /// <summary>
        /// The amount of points in this transaction
        /// </summary>
        public sealed class Of : ScalarEnvelope<double>
        {
            /// <summary>
            /// The amount of points in this transaction
            /// </summary>
            public Of(IEntity transaction) : base(()=>
                transaction.Memory().Prop<double>("amount")
            )
            { }
        }
    }
}
