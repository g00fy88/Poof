using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The description of the quest
    /// </summary>
    public sealed class Note : EntityInputEnvelope
    {
        /// <summary>
        /// The description of the quest
        /// </summary>
        public Note(string value) : base(floor =>
            floor.Update("note", value)
        )
        { }

        /// <summary>
        /// The description of the quest
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The description of the quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("note"),
                false
            )
            { }
        }
    }
}
