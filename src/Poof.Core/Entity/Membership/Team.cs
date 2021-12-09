using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Membership
{
    /// <summary>
    /// The fellowship, which the membership belongs to
    /// </summary>
    public sealed class Team : EntityInputEnvelope
    {
        /// <summary>
        /// The fellowship, which the membership belongs to
        /// </summary>
        public Team(string fellowship) : base(floor =>
            floor.Update("team", fellowship)
        )
        { }

        /// <summary>
        /// The fellowship, which the membership belongs to
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The fellowship, which the membership belongs to
            /// </summary>
            public Of(IEntity membership) : base(() =>
                membership.Memory().Prop<string>("team"),
                false
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string fellowship) : base(
                "team",
                "equals",
                fellowship
            )
            { }
        }
    }
}
