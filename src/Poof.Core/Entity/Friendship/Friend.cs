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
    public sealed class Friend : EntityInputEnvelope
    {
        /// <summary>
        /// The user that holds the membership
        /// </summary>
        public Friend(string user) : base(floor =>
            floor.Update("friend", user)
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
                frienship.Memory().Prop<string>("friend"),
                false
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string user) : base(
                "friend",
                "equals",
                user
            )
            { }
        }
    }
}
