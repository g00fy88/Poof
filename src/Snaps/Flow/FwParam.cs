using Poof.Snaps.Opt;

namespace Poof.Snaps.Flow
{
    /// <summary>
    /// A Flow that matches a param and returns the response if equal
    /// </summary>
    /// <param name="name"></param>
    /// <param name="param"></param>
    /// <param name="flows"></param>
    public sealed class FwParam<TResult> : IFlow<TResult>
    {
        private readonly string name;
        private readonly string param;
        private readonly IFlow<TResult> flow;

        /// <summary>
        /// A Flow that matches a param and returns the response if equal
        /// </summary>
        /// <param name="name"></param>
        /// <param name="param"></param>
        /// <param name="target"></param>
        public FwParam(string name, string param, ISnap<TResult> target) : this(name, param, new FwResponse<TResult>(target))
        { }

        /// <summary>
        /// A Flow that matches a param and returns the response if equal.
        /// </summary>
        public FwParam(string name, string param, params IFlow<TResult>[] flows) : this(name, param, new FwChain<TResult>(flows))
        { }

        /// <summary>
        /// A Flow that matches a param and returns the response if equal.
        /// </summary>
        public FwParam(string name, string param, IFlow<TResult> flow)
        {
            this.name = name;
            this.param = param;
            this.flow = flow;
        }

        public IOpt<TResult> Response(IDemand demand)
        {
            IOpt<TResult> result = new OptNone<TResult>();
            if (demand.Param(this.name) == this.param)
            {
                result = this.flow.Response(demand);
            }
            return result;
        }
    }
}
