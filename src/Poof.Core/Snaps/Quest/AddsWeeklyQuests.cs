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
    /// Checks if there are private quests of the given user.
    /// If there are quests that are open and the end-date is still in future,
    /// the use case is rescheduled to this end-date.
    /// If not, these quests are removed and new quests are added for this user
    /// with an end-date in 7 days. this use case is rescheduled to this date as well.
    /// </summary>
    public sealed class AddsWeeklyQuests : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Checks if there are private quests of the given user.
        /// If there are quests that are open and the end-date is still in future,
        /// the use case is rescheduled to this end-date.
        /// If not, these quests are removed and new quests are added for this user
        /// with an end-date in 7 days. this use case is rescheduled to this date as well.
        /// </summary>
        public AddsWeeklyQuests(IDataBuilding mem, IIdentity identity, IFuture future) : base(dmd =>
        {
            var quests = new Quests(mem);
            var privateQuests =
                quests.List(
                    new Scope.Match("private"),
                    new Issuer.Match(identity.UserID())
                );

            var date = DateTime.Now;
            bool hasOpenQuests = false;
            foreach(var id in privateQuests)
            {
                var quest = new QuestOf(mem, id);
                if(new Status.Of(quest).AsString() == "open")
                {
                    if(new EndDate.Of(quest).Value() > date)
                    {
                        hasOpenQuests = true;
                        date = new EndDate.Of(quest).Value();
                    }
                    else
                    {
                        quests.Remove(id);
                    }
                }
            }

            if(!hasOpenQuests)
            {
                date = date.AddDays(7);
                // create new quests;
            }

            future.Schedule(date,
                new JobOf(
                    identity,
                    new DmAddWeeklyQuests()
                )
            );
        })
        { }
    }
}
