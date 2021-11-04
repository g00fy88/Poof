using Poof.Snaps.Opt;

namespace Poof.Snaps.Flow
{
    /// <summary>
    /// A flow which directs to a snap.
    /// </summary>
    public sealed class FwResponse<TResult> : IFlow<TResult>
    {
        private readonly ISnap<TResult> snap;

        /// <summary>
        /// A flow which directs to a snap.
        /// </summary>
        public FwResponse(ISnap<TResult> snap)
        {
            this.snap = snap;
        }

        /// <summary>
        /// A flow which directs to a snap.
        /// </summary>
        public IOpt<TResult> Response(IDemand demand)
        {
            return new OptOf<TResult>(this.snap.Convert(demand));
        }
    }
}
