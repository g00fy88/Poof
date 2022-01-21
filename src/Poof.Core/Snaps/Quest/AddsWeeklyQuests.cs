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
    public sealed class AddsWeeklyQuests : SnapEnvelope<IInput>
    {
        public AddsWeeklyQuests(IDataBuilding mem, IIdentity identity, IFuture future) : base(dmd =>
        {
            var quests = new Quests(mem);
            var privateQuests =
                quests.List(
                    new Scope.Match("private"),
                    new Issuer.Match(identity)
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
