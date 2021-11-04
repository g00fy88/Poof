using Poof.Snaps.Opt;

namespace Poof.Snaps.Flow
{
    /// <summary>
    /// Executes all flows until a flow has a result. Stops after that.
    /// </summary>
    public sealed class FwChain<TResult> : IFlow<TResult>
    {
        private readonly IFlow<TResult>[] routes;

        /// <summary>
        /// Executes all flows until a flow has a result. Stops after that.
        /// </summary>
        public FwChain(params IFlow<TResult>[] routes)
        {
            this.routes = routes;
        }

        public IOpt<TResult> Response(IDemand demand)
        {
            IOpt<TResult> response = new OptNone<TResult>();
            foreach (var route in this.routes)
            {
                var result = route.Response(demand);
                if (result.Has())
                {
                    response = result;
                    break;
                }
            }
            return response;
        }
    }
}
