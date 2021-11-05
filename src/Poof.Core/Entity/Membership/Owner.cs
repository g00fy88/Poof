using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Membership
{
    /// <summary>
    /// The user that holds the membership
    /// </summary>
    public sealed class Owner : EntityInputEnvelope
    {
        /// <summary>
        /// The user that holds the membership
        /// </summary>
        public Owner(string user) : base(floor =>
            floor.Update("owner", user)
        )
        { }

        /// <summary>
        /// The user that holds the membership
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The user that holds the membership
            /// </summary>
            public Of(IEntity membership) : base(()=>
                membership.Memory().Prop<string>("owner"),
                false
            )
            { }
        }
    }
}
