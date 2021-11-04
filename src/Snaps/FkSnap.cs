using Poof.Snaps.Outcome;
using System;

namespace Poof.Snaps
{
    /// <summary>
    /// A fake snap.
    /// </summary>
    public sealed class FkSnap<TResult> : ISnap<TResult>
    {
        private readonly Func<IDemand, IOutcome<TResult>> act;

        /// <summary>
        /// A fake snap which returns an empty opt.
        /// </summary>
        public FkSnap() : this(demand => new EmptyOutcome<TResult>())
        { }

        /// <summary>
        /// A fake snap which executes the given function when converting.
        /// </summary>
        public FkSnap(Func<IDemand, IOutcome<TResult>> act)
        {
            this.act = act;
        }

        public IOutcome<TResult> Convert(IDemand demand)
        {
            return act.Invoke(demand);
        }
    }
}


