using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The contact infos of the quest, if needed.
    /// Some quests require information exchange, 
    /// if so, the issuer describes how to get in contact
    /// </summary>
    public sealed class Contact : EntityInputEnvelope
    {
        /// <summary>
        /// The contact infos of the quest, if needed.
        /// Some quests require information exchange, 
        /// if so, the issuer describes how to get in contact
        /// </summary>
        public Contact(bool needed, string value) : base(floor =>
        {
            floor.Update("contact-needed", needed);
            floor.Update("contact", value);
        })
        { }

        /// <summary>
        /// The contact infos of the quest, if needed.
        /// Some quests require information exchange, 
        /// if so, the issuer describes how to get in contact
        /// </summary>
        public sealed class Needed : ScalarEnvelope<bool>
        {
            /// <summary>
            /// The contact infos of the quest, if needed.
            /// Some quests require information exchange, 
            /// if so, the issuer describes how to get in contact
            /// </summary>
            public Needed(IEntity quest) : base(() =>
                quest.Memory().Prop<bool>("contact-needed")
            )
            { }
        }

        /// <summary>
        /// The contact infos of the quest, if needed.
        /// Some quests require information exchange, 
        /// if so, the issuer describes how to get in contact
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The contact infos of the quest, if needed.
            /// Some quests require information exchange, 
            /// if so, the issuer describes how to get in contact
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("contact"),
                false
            )
            { }
        }
    }
}
