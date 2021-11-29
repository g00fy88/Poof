using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Transaction
{
    /// <summary>
    /// The Title of the transaction
    /// </summary>
    public sealed class Title : EntityInputEnvelope
    {
        /// <summary>
        /// The Title of the transaction
        /// </summary>
        public Title(string text) : base(floor =>
            floor.Update("title", text)
        )
        { }

        /// <summary>
        /// The Title of the transaction
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The Title of the transaction
            /// </summary>
            public Of(IEntity transaction) : base(()=>
                transaction.Memory().Prop<string>("title"),
                false
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(string title) : base(
                "title",
                "equals",
                title
            )
            { }
        }
    }
}
