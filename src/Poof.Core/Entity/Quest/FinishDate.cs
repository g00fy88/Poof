using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The end date of the quest
    /// </summary>
    public sealed class FinishDate : EntityInputEnvelope
    {
        /// <summary>
        /// The end date of the quest
        /// </summary>
        public FinishDate(DateTime date) : base(floor =>
            floor.Update("finish-date", date)
        )
        { }

        /// <summary>
        /// The end date of the quest
        /// </summary>
        public sealed class Of : ScalarEnvelope<DateTime>
        {
            /// <summary>
            /// The end date of the quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<DateTime>("finish-date")
            )
            { }
        }
    }
}
