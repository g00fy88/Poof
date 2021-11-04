using System;
using System.Collections.Generic;
using Yaapii.Atoms;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;

namespace Poof.Snaps
{
    /// <summary>
    /// A Demand that has no content
    /// </summary>
    public sealed class EmptyDemand : IDemand
    {
        /// <summary>
        /// A Demand that has no content
        /// </summary>
        public EmptyDemand()
        { }

        public IInput Body()
        {
            return new DeadInput();
        }

        public string Param(string name)
        {
            throw new ArgumentException($"Cannot access parameter '{name}' from an empty demand.");
        }

        public string Param(string name, string def)
        {
            return def;
        }

        public IList<string> Params()
        {
            return new ListOf<string>();
        }

        public IDemand Refined(string param, string value)
        {
            return new DemandOf(new DeadInput()).Refined(param, value);
        }
    }
}
