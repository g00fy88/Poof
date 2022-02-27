using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Friendship
{
    /// <summary>
    /// The user that holds the membership
    /// </summary>
    public sealed class Requester : EntityInputEnvelope
    {
        /// <summary>
        /// The user that holds the membership
        /// </summary>
        public Requester(string user) : base(floor =>
            floor.Update("requester", user)
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
            public Of(IEntity frienship) : base(()=>
                frienship.Memory().Prop<string>("requester"),
                false
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string user) : base(
                "requester",
                "equals",
                user
            )
            { }
        }
    }
}
