using Poof.Snaps.Outcome;
using System;

namespace Poof.Snaps
{
    /// <summary>
    /// Envelope for snaps
    /// </summary>
    public abstract class SnapEnvelope<TResult> : ISnap<TResult>
    {
        private readonly Func<IDemand, IOutcome<TResult>> solve;

        /// <summary>
        /// Envelope for snaps
        /// </summary>
        public SnapEnvelope(Action<IDemand> solve) : this((demand) =>
        {
            solve(demand);
            return new EmptyOutcome<TResult>();
        })
        { }

        /// <summary>
        /// Envelope for snaps
        /// </summary>
        public SnapEnvelope(Func<IDemand, IOutcome<TResult>> solve)
        {
            this.solve = solve;
        }

        public IOutcome<TResult> Convert(IDemand demand)
        {
            return this.solve(demand);
        }
    }
}
