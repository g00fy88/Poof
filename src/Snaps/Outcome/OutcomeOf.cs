using System;
using System.Collections.Generic;
using Yaapii.Atoms.List;

namespace Poof.Snaps.Outcome
{
    /// <summary>
    /// Simple outcome with a result.
    /// </summary>
    public sealed class OutcomeOf<TResult> : IOutcome<TResult>
    {
        private readonly TResult result;
        private readonly IDictionary<string, string> parameters;

        /// <summary>
        /// Simple outcome with a result.
        /// </summary>
        public OutcomeOf(TResult result) : this(result, "unknown")
        { }

        /// <summary>
        /// Simple outcome with a result.
        /// </summary>
        public OutcomeOf(TResult result, string format) : this(result, new Dictionary<string, string>() { { "format", format } })
        { }

        /// <summary>
        /// Simple outcome with a result.
        /// </summary>
        private OutcomeOf(TResult result, IDictionary<string, string> parameters)
        {
            this.result = result;
            this.parameters = parameters;
        }

        public string Param(string name)
        {
            if (parameters.ContainsKey(name))
            {
                return parameters[name];
            }
            throw new ArgumentException($"Parameter '{name}' does not exist. Valid parameters are: {string.Join(", ", parameters.Keys)}");
        }

        public IList<string> Params()
        {
            return new ListOf<string>(parameters.Keys);
        }

        public bool IsEmpty()
        {
            return false;
        }

        public IOutcome<TResult> Refined(string param, string value)
        {
            var parameters = this.parameters;
            parameters[param] = value;
            return new OutcomeOf<TResult>(result, parameters);
        }

        public TResult Result()
        {
            return result;
        }
    }
}
