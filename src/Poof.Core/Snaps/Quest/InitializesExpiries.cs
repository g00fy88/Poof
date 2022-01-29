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
    public sealed class InitializesExpiries : SnapEnvelope<IInput>
    {
        public InitializesExpiries(IDataBuilding mem, IFuture future) : base(dmd =>
        {
            foreach(var id in new Quests(mem).List(new Status.Match("pending")))
            {
                var quest = new QuestOf(mem, id);
                if (new Applicant.Has(quest).Value())
                {
                    var expiryDate = new Applicant.StartDate(quest).Value().AddHours(new CompletionTime.Of(quest).Value());
                    future.Schedule(
                        new JobOf(
                            expiryDate,
                            new DmFailWhenUnfinished(id)
                        )
                    );
                }
            }
        })
        { }
    }
}
