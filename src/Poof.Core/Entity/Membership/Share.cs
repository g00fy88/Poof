using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Membership
{
    /// <summary>
    /// The share which the user has of the fellowship in this membership
    /// </summary>
    public sealed class Share : EntityInputEnvelope
    {
        /// <summary>
        /// The share which the user has of the fellowship in this membership
        /// </summary>
        public Share(double share) : base(floor =>
            floor.Update("share", share)
        )
        { }

        /// <summary>
        /// The share which the user has of the fellowship in this membership
        /// </summary>
        public sealed class Of : ScalarEnvelope<double>
        {
            /// <summary>
            /// The share which the user has of the fellowship in this membership
            /// </summary>
            public Of(IEntity membership) : base(()=>
                membership.Memory().Prop<double>("share")
            )
            { }
        }
    }
}
