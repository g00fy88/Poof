using Newtonsoft.Json.Linq;
using Poof.Core.Entity.Quest;
using Poof.Core.Future;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Future;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using Poof.Talk.Snaps.Quest;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Quest
{
    /// <summary>
    /// The picture url of the given quest
    /// </summary>
    public sealed class GetsPicture : SnapEnvelope<IInput>
    {
        /// <summary>
        /// The picture url of the given quest
        /// </summary>
        public GetsPicture(IDataBuilding mem) : base(dmd =>
        {
            var questId = dmd.Param("quest");
            var quest = new QuestOf(mem, questId);

            return 
                new JsonRawOutcome(
                    new JObject(
                        new JProperty("url", new Picture.Url(quest).AsString())
                    )
                );
        })
        { }
    }
}
