using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The status of the quest
    /// </summary>
    public sealed class Status : EntityInputEnvelope
    {
        /// <summary>
        /// The status of the quest
        /// </summary>
        public Status(string value) : base(floor =>
            floor.Update("status", value)
        )
        { }

        /// <summary>
        /// The status of the quest
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The status of the quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("status"),
                false
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string scope) : base(
                "status",
                "equals",
                scope
            )
            { }
        }
    }
}
