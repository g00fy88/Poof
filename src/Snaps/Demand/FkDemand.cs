using System;
using System.Collections.Generic;
using Yaapii.Atoms;
using Yaapii.Atoms.IO;

#pragma warning disable MaxVariablesCount // Four fields maximum

namespace Poof.Snaps.Demand
{
    /// <summary>
    /// A Fake Demand
    /// </summary>
    public sealed class FkDemand : IDemand
    {
        private readonly Func<IInput> body;
        private readonly Func<string, string, string> paramWithDefault;
        private readonly Func<string, string> param;
        private readonly Func<IList<string>> parameters;
        private readonly Func<string, string, IDemand> refined;

        /// <summary>
        /// A Fake Demand
        /// </summary>
        public FkDemand() : this(
            new Func<IInput>(() => new DeadInput()),
            new Func<string, string, string>((a, b) => string.Empty),
            new Func<string, string>((a) => string.Empty),
            new Func<IList<string>>(() => new List<string>()),
            new Func<string, string, IDemand>((a, b) => new EmptyDemand())
        )
        { }

        /// <summary>
        /// A Fake Demand
        /// </summary>
        public FkDemand(Func<string, string, IDemand> refined) : this(
            new Func<IInput>(() => new DeadInput()),
            new Func<string, string, string>((a, b) => string.Empty),
            new Func<string, string>((a) => string.Empty),
            new Func<IList<string>>(() => new List<string>()),
            refined
        )
        { }

        /// <summary>
        /// A Fake Demand
        /// </summary>
        public FkDemand(Func<IList<string>> parameters) : this(
            new Func<IInput>(() => new DeadInput()),
            new Func<string, string, string>((a, b) => string.Empty),
            new Func<string, string>((a) => string.Empty),
            parameters,
            new Func<string, string, IDemand>((a, b) => new EmptyDemand())
        )
        { }

        /// <summary>
        /// A Fake Demand
        /// </summary>
        public FkDemand(Func<string, string, string> param) : this(
            new Func<IInput>(() => new DeadInput()),
            param,
            new Func<string, string>((a) => string.Empty),
            new Func<IList<string>>(() => new List<string>()),
            new Func<string, string, IDemand>((a, b) => new EmptyDemand())
        )
        { }

        /// <summary>
        /// A Fake Demand
        /// </summary>
        public FkDemand(Func<IInput> body) : this(
            body,
            new Func<string, string, string>((a, b) => string.Empty),
            new Func<string, string>((a) => string.Empty),
            new Func<IList<string>>(() => new List<string>()),
            new Func<string, string, IDemand>((a, b) => new EmptyDemand())
        )
        { }

        /// <summary>
        /// A Fake Demand
        /// </summary>
        public FkDemand(
            Func<IInput> body,
            Func<string, string, string> paramWithDefault,
            Func<string, string> param,
            Func<IList<string>> parameters,
            Func<string, string, IDemand> refined)
        {
            this.body = body;
            this.paramWithDefault = paramWithDefault;
            this.param = param;
            this.parameters = parameters;
            this.refined = refined;
        }

        public IInput Body()
        {
            return body();
        }

        public string Param(string name)
        {
            return param(name);
        }

        public string Param(string name, string def)
        {
            return paramWithDefault(name, def);
        }

        public IList<string> Params()
        {
            return parameters();
        }

        public IDemand Refined(string param, string value)
        {
            return refined(param, value);
        }
    }
}
