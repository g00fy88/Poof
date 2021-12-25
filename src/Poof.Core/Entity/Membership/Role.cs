using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Membership
{
    /// <summary>
    /// The role of the user as member of team
    /// </summary>
    public sealed class Role : EntityInputEnvelope
    {
        /// <summary>
        /// The role of the user as member of team
        /// </summary>
        public Role(string role) : base(floor =>
            floor.Update("role", role)
        )
        { }

        /// <summary>
        /// The role of the user as member of team
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The role of the user as member of team
            /// </summary>
            public Of(IEntity membership) : base(()=>
                membership.Memory().Prop<string>("role"),
                false
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string user) : base(
                "role",
                "equals",
                user
            )
            { }
        }
    }
}
