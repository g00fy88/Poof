using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The category of the quest
    /// </summary>
    public sealed class Category : EntityInputEnvelope
    {
        /// <summary>
        /// The category of the quest
        /// </summary>
        public Category(string value) : base(floor =>
            floor.Update("category", value)
        )
        { }

        /// <summary>
        /// The category of the quest
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The category of the quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("category"),
                false
            )
            { }
        }
    }
}
