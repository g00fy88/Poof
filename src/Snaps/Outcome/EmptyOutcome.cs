using System;
using System.Collections.Generic;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Map;

namespace Poof.Snaps.Outcome
{
    /// <summary>
    /// A Outcome that is empty. No result only parameters.
    /// </summary>
    public sealed class EmptyOutcome<TResult> : IOutcome<TResult>
    {
        private readonly IDictionary<string, string> parameters;

        /// <summary>
        /// A Outcome that is empty. No result only parameters.
        /// </summary>
        public EmptyOutcome() : this(new MapOf("format", "empty"))
        { }

        /// <summary>
        /// A Outcome that is empty. No result only parameters.
        /// </summary>
        private EmptyOutcome(IDictionary<string, string> parameters)
        {
            this.parameters = new Dictionary<string, string>(parameters);
        }

        public string Param(string name)
        {
            if (parameters.ContainsKey(name))
            {
                return parameters[name];
            }
            throw new ArgumentException($"Cannot deliver non existing parameter '{name}', valid parameters are: {string.Join(", ", this.parameters.Keys)}");
        }

        public IList<string> Params()
        {
            return new ListOf<string>(parameters.Keys);
        }

        public IOutcome<TResult> Refined(string param, string value)
        {
            var parameters = this.parameters;
            parameters[param] = value;
            return new EmptyOutcome<TResult>(parameters);
        }

        public bool IsEmpty()
        {
            return true;
        }

        public TResult Result()
        {
            throw new InvalidOperationException($"Cannot get result from an emtpy outcome.");
        }
    }
}
