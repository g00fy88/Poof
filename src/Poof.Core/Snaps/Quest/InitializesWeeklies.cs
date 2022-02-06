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
    public sealed class InitializesWeeklies : SnapEnvelope<IInput>
    {
        public InitializesWeeklies(IDataBuilding mem, IFuture future) : base(dmd =>
        {
            //foreach (var id in new Quests(mem).List())
            //{
            //    new Quests(mem).Remove(id);
            //}
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
