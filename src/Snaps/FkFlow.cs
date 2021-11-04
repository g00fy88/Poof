using Poof.Snaps.Opt;
using System;

namespace Poof.Snaps
{
    /// <summary>
    /// A fake flow.
    /// </summary>
    public sealed class FkFlow<TResult> : IFlow<TResult>
    {
        private readonly Func<IDemand, IOpt<TResult>> response;

        /// <summary>
        /// A fake flow with a <see cref="Snaps.Opt.OptNone"/> response.
        /// </summary>
        public FkFlow() : this(new OptNone<TResult>())
        { }

        /// <summary>
        /// A fake flow.
        /// </summary>
        public FkFlow(IOpt<TResult> response) : this(demand => response)
        { }

        /// <summary>
        /// A fake flow.
        /// </summary>
        public FkFlow(Func<IDemand, IOpt<TResult>> response)
        {
            this.response = response;
        }

        public IOpt<TResult> Response(IDemand demand)
        {
            return this.response(demand);
        }
    }
}
