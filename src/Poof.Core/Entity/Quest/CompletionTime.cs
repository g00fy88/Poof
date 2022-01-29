using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The end date of the quest
    /// </summary>
    public sealed class CompletionTime : EntityInputEnvelope
    {
        /// <summary>
        /// The end date of the quest
        /// </summary>
        public CompletionTime(double hours) : base(floor =>
            floor.Update("completion-time", hours)
        )
        { }

        /// <summary>
        /// The end date of the quest
        /// </summary>
        public sealed class Of : ScalarEnvelope<double>
        {
            /// <summary>
            /// The end date of the quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<double>("completion-time")
            )
            { }
        }
    }
}
