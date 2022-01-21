﻿using Poof.Core.Model.Entity;
using System;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The description of the quest
    /// </summary>
    public sealed class Description : EntityInputEnvelope
    {
        /// <summary>
        /// The description of the quest
        /// </summary>
        public Description(string value) : base(floor =>
            floor.Update("description", value)
        )
        { }

        /// <summary>
        /// The description of the quest
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The description of the quest
            /// </summary>
            public Of(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("description"),
                false
            )
            { }
        }
    }
}
