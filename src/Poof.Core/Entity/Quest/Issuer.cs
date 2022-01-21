using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The user, that had issued this quest
    /// </summary>
    public sealed class Issuer : EntityInputEnvelope
    {
        /// <summary>
        /// The user, that had issued this quest
        /// </summary>
        public Issuer(string user) : base(floor =>
            floor.Update("issuer", user)
        )
        { }

        /// <summary>
        /// The user, that had issued this quest
        /// </summary>
        public sealed class Of : ScalarEnvelope<string>
        {
            /// <summary>
            /// The user, that had issued this quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("issuer")
            )
            { }
        }

        public sealed class Match : PropMatchEnvelope
        {
            public Match(IIdentity identity) : base(
                "issuer",
                "equals",
                identity
            )
            { }
        }
    }
}
