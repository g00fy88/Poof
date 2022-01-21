using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The user, that had applied for this quest
    /// </summary>
    public sealed class Applicant : EntityInputEnvelope
    {
        /// <summary>
        /// The user, that had applied for this quest
        /// </summary>
        public Applicant(string user) : base(floor =>
            floor.Update("applicants", new string[] {user})
        )
        { }

        /// <summary>
        /// The user, that had applied for this quest
        /// </summary>
        public sealed class Has : ScalarEnvelope<bool>
        {
            /// <summary>
            /// The user, that had applied for this quest
            /// </summary>
            public Has(IEntity quest) : base(()=>
                quest.Memory().Prop<string[]>("applicants").Length > 0
            )
            { }
        }

        /// <summary>
        /// The user, that had applied for this quest
        /// </summary>
        public sealed class Of : ScalarEnvelope<string>
        {
            /// <summary>
            /// The user, that had applied for this quest
            /// </summary>
            public Of(IEntity quest) : base(() =>
                new FirstOf<string>(
                    quest.Memory().Prop<string[]>("applicants"),
                    new InvalidOperationException("Unable to retrieve applicant of quest, because there is no applicant yet")
                ).Value()
            )
            { }
        }
    }
}
