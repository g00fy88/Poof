using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms;
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
        public Applicant(string user) : this(
            user,
            new ScalarOf<DateTime>(() => DateTime.Now)
        )
        { }

        /// <summary>
        /// The user, that had applied for this quest
        /// </summary>
        public Applicant(string user, IScalar<DateTime> date) : base(floor =>
        {
            floor.Update("applicants", new string[] { user });
            floor.Update("apply-date", date.Value());
        }
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

        /// <summary>
        /// The user, that had applied for this quest
        /// </summary>
        public sealed class StartDate : ScalarEnvelope<DateTime>
        {
            /// <summary>
            /// The user, that had applied for this quest
            /// </summary>
            public StartDate(IEntity quest) : base(() =>
                quest.Memory().Prop<DateTime>("apply-date")
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string user) : base(
                "applicant",
                "equals",
                user
            )
            { }
        }
    }
}
