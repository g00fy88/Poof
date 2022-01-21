using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The description of the quest
    /// </summary>
    public sealed class Reward : EntityInputEnvelope
    {
        /// <summary>
        /// The description of the quest
        /// </summary>
        public Reward(double value) : base(floor =>
            floor.Update("reward", value)
        )
        { }

        /// <summary>
        /// The description of the quest
        /// </summary>
        public sealed class Of : ScalarEnvelope<double>
        {
            /// <summary>
            /// The description of the quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<double>("reward")
            )
            { }
        }
    }
}
