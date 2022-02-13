using Poof.Core.Entity.Quest;
using Poof.Core.Entity.User;
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
    public sealed class InitializesQuests : SnapEnvelope<IInput>
    {
        public InitializesQuests(IDataBuilding mem, IFuture future) : base(dmd =>
        {
            var openQuests =
                new Quests(mem).List(
                    new Status.Match("open"),
                    new Scope.NoMatch("private")
                );
            foreach (var quest in openQuests)
            {
                future.Schedule(
                    new JobOf(
                        new EndDate.Of(new QuestOf(mem, quest)).Value(),
                        new DmRemoveUnapplied(quest)
                    )
                );
            }
            foreach (var id in new Users(mem).List())
            {
                future.Schedule(
                    new JobOf(
                        new DmAddWeeklyQuests(id)
                    )
                );
            }
        })
        { }
    }
}
