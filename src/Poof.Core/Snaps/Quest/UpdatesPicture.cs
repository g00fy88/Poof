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
    /// Adds the picture in the demand to the given quest
    /// </summary>
    public sealed class UpdatesPicture : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Adds the picture in the demand to the given quest
        /// </summary>
        public UpdatesPicture(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var questId = dmd.Param("quest");
            var quest = new QuestOf(mem, questId);

            if(new Issuer.Of(quest).Value() != identity.UserID())
            {
                throw new InvalidOperationException($"Unable to update picture of quest '{questId}', " +
                    $"because the quest issuer does not match the identity. Only the issuer of a quest can update its content.");
            }

            quest.Update(
                new Picture(dmd.Body())
            );

            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
