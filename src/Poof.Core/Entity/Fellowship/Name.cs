using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Fellowship
{
    /// <summary>
    /// The name of the fellowship
    /// </summary>
    public sealed class Name : EntityInputEnvelope
    {
        /// <summary>
        /// The name of the fellowship
        /// </summary>
        public Name(string name) : base(mem =>
            mem.Update("name", name)
        )
        { }

        /// <summary>
        /// The name of the fellowship
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The name of the fellowship
            /// </summary>
            public Of(IEntity fellowship) : base(() =>
                fellowship.Memory().Prop<string>("name"),
                false
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string name) : base(
                "name",
                "equals",
                name
            )
            { }
        }
    }
}
