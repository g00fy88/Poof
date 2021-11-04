using System.Collections.Generic;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace Poof.Snaps.Outcome
{
    /// <summary>
    /// Envelope for outcome.
    /// </summary>
    public abstract class OutcomeEnvelope<TResult> : IOutcome<TResult>
    {
        private readonly IScalar<IOutcome<TResult>> origin;

        /// <summary>
        /// Envelope for outcome.
        /// </summary>
        public OutcomeEnvelope(IOutcome<TResult> origin) : this(
            new ScalarOf<IOutcome<TResult>>(
                () => origin
            )
        )
        { }

        /// <summary>
        /// Envelope for outcome.
        /// </summary>
        public OutcomeEnvelope(IScalar<IOutcome<TResult>> outcome)
        {
            this.origin = new ScalarOf<IOutcome<TResult>>(outcome);
        }

        public string Param(string name)
        {
            return this.origin.Value().Param(name);
        }

        public IList<string> Params()
        {
            return this.origin.Value().Params();
        }

        public bool IsEmpty()
        {
            return this.origin.Value().IsEmpty();
        }

        public IOutcome<TResult> Refined(string param, string value)
        {
            return this.origin.Value().Refined(param, value);
        }

        public TResult Result()
        {
            return this.origin.Value().Result();
        }
    }
}
