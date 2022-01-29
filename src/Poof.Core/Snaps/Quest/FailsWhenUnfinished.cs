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

namespace Poof.Core.Snaps.Quest
{
    /// <summary>
    /// Checks if the quest in the demand is expired still not finished.
    /// If so, the quest status is changed to failed
    /// </summary>
    public sealed class FailsWhenUnfinished : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Checks if the quest in the demand is expired still not finished.
        /// If so, the quest status is changed to failed. If not expired, it reschedules itself with the expiry date
        /// </summary>
        public FailsWhenUnfinished(IDataBuilding mem, IFuture future) : base(dmd =>
        {
            var questId = dmd.Param("quest");
            var quest = new QuestOf(mem, questId);
            var date = DateTime.Now;
            if(
                new Applicant.Has(quest).Value() &&
                new Status.Of(quest).AsString() == "pending"
            )
            {
                var expiryDate = new Applicant.StartDate(quest).Value().AddHours(new CompletionTime.Of(quest).Value());
                if(expiryDate < date)
                {
                    quest.Update(
                        new Status("failed")
                    );
                }
                else
                {
                    future.Schedule(
                        new JobOf(
                            expiryDate,
                            new DmFailWhenUnfinished(questId)
                        )
                    );
                }
            }
        })
        { }
    }
}
