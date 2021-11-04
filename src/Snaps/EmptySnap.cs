using Poof.Snaps.Outcome;

namespace Poof.Snaps
{
    /// <summary>
    /// Converts to empty outcome.
    /// </summary>
    public sealed class EmptySnap<TResult> : ISnap<TResult>
    {
        /// <summary>
        /// Converts to empty outcome.
        /// </summary>
        public EmptySnap()
        { }

        /// <summary>
        /// Converts to empty outcome.
        /// </summary>
        public IOutcome<TResult> Convert(IDemand demand)
        {
            return new EmptyOutcome<TResult>();
        }
    }
}
