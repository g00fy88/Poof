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
    /// Adds the requesting user as applicant to the quest, given in the demand.
    /// Schedules a job to fail this quest, when it is not finished after completion time.
    /// </summary>
    public sealed class AddsApplicant : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Adds the requesting user as applicant to the quest, given in the demand.
        /// Schedules a job to fail this quest, when it is not finished after completion time.
        /// </summary>
        public AddsApplicant(IDataBuilding mem, IIdentity identity, IFuture future) : base(dmd =>
        {
            var questId = dmd.Param("quest");
            var quest = new QuestOf(mem, questId);
            if(new Scope.Of(quest).AsString() == "private" && new Issuer.Of(quest).Value() != identity.UserID())
            {
                throw new InvalidOperationException($"Unable to add applicant to quest '{dmd.Param("quest")}', " +
                    $"because the scope of this quest is 'private' and the issuer does not match the requesting user.");
            }

            quest.Update(
                new Applicant(identity.UserID()),
                new Status("pending")
            );

            future.Schedule(
                new JobOf(
                    new Applicant.StartDate(quest).Value().AddHours(new CompletionTime.Of(quest).Value()),
                    new DmFailWhenUnfinished(questId)
                )
            );
        })
        { }
    }
}
