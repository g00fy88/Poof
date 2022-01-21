using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The location of the quest, if needed.
    /// Some quests are bound to a specific location, 
    /// if so the location value describes this place
    /// </summary>
    public sealed class Location : EntityInputEnvelope
    {
        /// <summary>
        /// The location of the quest, if needed.
        /// Some quests are bound to a specific location, 
        /// if so the location value describes this place
        /// </summary>
        public Location(bool needed, string value) : base(floor =>
        {
            floor.Update("location-needed", needed);
            floor.Update("location", value);
        })
        { }

        /// <summary>
        /// The location of the quest, if needed.
        /// Some quests are bound to a specific location, 
        /// if so the location value describes this place
        /// </summary>
        public sealed class Needed : ScalarEnvelope<bool>
        {
            /// <summary>
            /// The location of the quest, if needed.
            /// Some quests are bound to a specific location, 
            /// if so the location value describes this place
            /// </summary>
            public Needed(IEntity quest) : base(() =>
                quest.Memory().Prop<bool>("location-needed")
            )
            { }
        }

        /// <summary>
        /// The location of the quest, if needed.
        /// Some quests are bound to a specific location, 
        /// if so the location value describes this place
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The location of the quest, if needed.
            /// Some quests are bound to a specific location, 
            /// if so the location value describes this place
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("location"),
                false
            )
            { }
        }
    }
}
