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
    /// Checks if the quest in the demand is expired and not yet applied
    /// If so, the quest is removed
    /// </summary>
    public sealed class RemovesUnapplied : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Checks if the quest in the demand is expired and not yet applied
        /// If so, the quest is removed
        /// </summary>
        public RemovesUnapplied(IDataBuilding mem, IFuture future) : base(dmd =>
        {
            var questId = dmd.Param("quest");
            var quest = new QuestOf(mem, questId);
            var date = DateTime.Now;
            if(
                !new Applicant.Has(quest).Value() &&
                new Status.Of(quest).AsString() == "open"
            )
            {
                var expiryDate = new EndDate.Of(quest).Value();
                if(expiryDate < date)
                {
                    new Quests(mem).Remove(questId);
                }
                else
                {
                    future.Schedule(
                        new JobOf(
                            expiryDate,
                            new DmRemoveUnapplied(questId)
                        )
                    );
                }
            }
        })
        { }
    }
}
