using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The end date of the quest
    /// </summary>
    public sealed class EndDate : EntityInputEnvelope
    {
        /// <summary>
        /// The end date of the quest
        /// </summary>
        public EndDate(DateTime date) : base(floor =>
        {
            floor.Update("end-date", date);
            floor.Update("has-end-date", true);
        })
        { }

        /// <summary>
        /// The end date of the quest
        /// </summary>
        public sealed class Has : ScalarEnvelope<bool>
        {
            /// <summary>
            /// The end date of the quest
            /// </summary>
            public Has(IEntity quest) : base(() =>
                quest.Memory().Prop<bool>("has-end-date")
            )
            { }
        }

        /// <summary>
        /// The end date of the quest
        /// </summary>
        public sealed class Of : ScalarEnvelope<DateTime>
        {
            /// <summary>
            /// The end date of the quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<DateTime>("end-date")
            )
            { }
        }
    }
}
