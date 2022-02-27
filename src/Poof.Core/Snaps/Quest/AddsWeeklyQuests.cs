using Poof.Core.Entity.Quest;
using Poof.Core.Entity.User;
using Poof.Core.Future;
using Poof.Core.Model.Data;
using Poof.Core.Model.Future;
using Poof.PrivateQuests;
using Poof.Snaps;
using Poof.Talk.Snaps.Quest;
using System;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;
using Yaapii.Xml;

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
        public AddsWeeklyQuests(IDataBuilding mem, IFuture future) : base(dmd =>
        {
            var quests = new Quests(mem);
            var user = dmd.Param("user");
            if (new Users(mem).List().Contains(user))
            {
                var privateQuests =
                    quests.List(
                        new Scope.Match("private"),
                        new Issuer.Match(user)
                    );

                var now = DateTime.Now;
                var date = DateTime.Now;
                bool hasOpenQuests = false;
                foreach (var id in privateQuests)
                {
                    var quest = new QuestOf(mem, id);

                    if (new Status.Of(quest).AsString() == "open")
                    {
                        if (new EndDate.Of(quest).Value() > now)
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

                if (!hasOpenQuests)
                {
                    date = date.AddDays(7);
                    foreach (var newQuest in new RandomQuests(3))
                    {
                        new QuestOf(mem, quests.New()).Update(
                            new Scope("private"),
                            new Status("open"),
                            new Issuer(user),
                            new Title(new XMLString(newQuest, "/quest/title/text()").Value()),
                            new CompletionTime(
                                new DoubleOf(new XMLString(newQuest, "/quest/completion-time/text()").Value()).Value()
                            ),
                            new EndDate(date),
                            new Category(new XMLString(newQuest, "/quest/category/text()").Value()),
                            new Description(new XMLString(newQuest, "/quest/description/text()").Value()),
                            new Reward(
                                new DoubleOf(new XMLString(newQuest, "/quest/reward/text()").Value()).Value()
                            ),
                            new Picture(new XMLString(newQuest, "/quest/pictureUrl/text()").Value())
                        );
                    }
                }

                future.Schedule(
                    new JobOf(
                        date,
                        new DmAddWeeklyQuests(user)
                    )
                );
            }
        })
        { }
    }
}
