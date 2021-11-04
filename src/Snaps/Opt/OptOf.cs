namespace Poof.Snaps.Opt
{
    /// <summary>
    /// A optional outcome that has a content
    /// </summary>
    public sealed class OptOf<TResult> : IOpt<TResult>
    {
        private readonly IOutcome<TResult> outcome;

        /// <summary>
        /// A optional outcome that has a outcome
        /// </summary>
        public OptOf(IOutcome<TResult> outcome)
        {
            this.outcome = outcome;
        }

        public bool Has()
        {
            return true;
        }

        public IOutcome<TResult> Value()
        {
            return outcome;
        }
    }
}
