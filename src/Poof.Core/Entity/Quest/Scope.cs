using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The scope of the quest
    /// 'private' means this quest can only be seen from the issuer.
    /// 'public' means this quest can be seen by everybody
    /// </summary>
    public sealed class Scope : EntityInputEnvelope
    {
        /// <summary>
        /// The scope of the quest
        /// 'private' means this quest can only be seen from the issuer.
        /// 'public' means this quest can be seen by everybody
        /// </summary>
        public Scope(string value) : base(floor =>
            floor.Update("scope", value)
        )
        { }

        /// <summary>
        /// The scope of the quest
        /// 'private' means this quest can only be seen from the issuer.
        /// 'public' means this quest can be seen by everybody
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The scope of the quest
            /// 'private' means this quest can only be seen from the issuer.
            /// 'public' means this quest can be seen by everybody
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("scope"),
                false
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string scope) : base(
                "scope",
                "equals",
                scope
            )
            { }
        }

        public sealed class NoMatch : PropMatchEnvelope
        {
            public NoMatch(string scope) : base(
                "scope",
                "not-equal",
                scope
            )
            { }
        }
    }
}
