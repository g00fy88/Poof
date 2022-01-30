using Poof.Core.Entity.Quest;
using Poof.Core.Future;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Future;
using Poof.Snaps;
using Poof.Talk.Snaps.Quest;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.JSON;

namespace Poof.Core.Snaps.Quest
{
    /// <summary>
    /// Checks if the quest in the demand is expired still not finished.
    /// If so, the quest status is changed to failed
    /// </summary>
    public sealed class FinishesQuest : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Checks if the quest in the demand is expired still not finished.
        /// If so, the quest status is changed to failed. If not expired, it reschedules itself with the expiry date
        /// </summary>
        public FinishesQuest(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var questId = dmd.Param("quest");
            var quest = new QuestOf(mem, questId);
            var date = DateTime.Now;

            if(!new Applicant.Has(quest).Value())
            {
                throw new InvalidOperationException($"Unable to finish quest '{quest}', because nobody applied yet.");
            }
            if(new Applicant.Of(quest).Value() != identity.UserID())
            {
                throw new InvalidOperationException($"Unable to finish quest '{quest}', because the applicant does not match the requesting user id." +
                    $" Only the applicant of a quest can finish it.");
            }

            quest.Update(
                new FinishDate(date),
                new Status("finished"),
                new Note(
                    new JSONOf(dmd.Body()).Value("note")
                )
            );
        })
        { }
    }
}
