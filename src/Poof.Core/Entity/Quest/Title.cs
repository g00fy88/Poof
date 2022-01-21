using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The title of the quest
    /// </summary>
    public sealed class Title : EntityInputEnvelope
    {
        /// <summary>
        /// The title of the quest
        /// </summary>
        public Title(string value) : base(floor =>
            floor.Update("title", value)
        )
        { }

        /// <summary>
        /// The title of the quest
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The title of the quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("title"),
                false
            )
            { }
        }
    }
}
