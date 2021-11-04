using System;
using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Map;
using Yaapii.JSON;

namespace Poof.Snaps
{
    /// <summary>
    /// A simple Demand that has no Params.
    /// </summary>
    public sealed class DemandOf : IDemand
    {
        private readonly IDictionary<string, string> parameters;
        private readonly IInput body;

        /// <summary>
        /// A demand with a body.
        /// </summary>
        public DemandOf(IJSON body) : this(
            new InputOf(
                body.Token().ToString()
            )
        )
        { }

        /// <summary>
        /// A demand with a body.
        /// </summary>
        public DemandOf(string[] parameters, IInput body) : this(new MapOf(() =>
        {
            if (parameters.Length % 2 != 0)
            {
                throw new ArgumentException("Cannot build a demand from an odd parameter count - expected a sequence of pairs (key=value)*n");
            }
            return new MapOf(parameters);
        }),
            new InputOf()
        )
        { }

        /// <summary>
        /// A demand with a body.
        /// </summary>
        public DemandOf(string key, string value, params string[] additionalParameters) : this(new MapOf(() =>
            {
                if (additionalParameters.Length % 2 != 0)
                {
                    throw new ArgumentException("Cannot build a demand from an odd parameter count - expected a sequence of pairs (key=value)*n");
                }
                var joined =
                    new Yaapii.Atoms.Enumerable.Joined<string>(
                        new ManyOf(key, value),
                        new ManyOf(additionalParameters)
                    ).ToArray();

                return new MapOf(joined);
            }),
            new InputOf()
        )
        { }

        /// <summary>
        /// A demand with a body.
        /// </summary>
        public DemandOf(IInput body) : this(new Dictionary<string, string>(), body)
        { }

        /// <summary>
        /// A demand with a body.
        /// </summary>
        private DemandOf(IDictionary<string, string> parameters, IInput body)
        {
            this.parameters = parameters;
            this.body = body;
        }

        public IInput Body()
        {
            return this.body;
        }

        public string Param(string name)
        {
            if (this.parameters.ContainsKey(name))
            {
                return this.parameters[name];
            }
            throw new ArgumentException($"Cannot access non existing parameter '{name}'.");
        }

        public string Param(string name, string def)
        {
            string param;
            if (this.parameters.ContainsKey(name))
            {
                param = this.parameters[name];
            }
            else
            {
                param = def;
            }
            return param;
        }

        public IList<string> Params()
        {
            return new List<string>(this.parameters.Keys);
        }

        public IDemand Refined(string param, string value)
        {
            var parameters = this.parameters;
            parameters[param] = value;
            return new DemandOf(parameters, this.body);
        }
    }
}
