using Poof.Core.Deal;
using Poof.Core.Entity.Quest;
using Poof.Core.Entity.Transaction;
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
using Yaapii.JSON;

namespace Poof.Core.Snaps.Quest
{
    /// <summary>
    /// Finishes the quest in the demand and rewards the applicant with the points and score by signing the poof deal
    /// </summary>
    public sealed class FinishesQuest : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Finishes the quest in the demand and rewards the applicant with the points and score by signing the poof deal
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
                new Status("finished"),
                new FinishDate(date),
                new Note(
                    new JSONOf(dmd.Body()).Value("note")
                )
            );

            new PoofDeal(mem, $"Quest: {new Entity.Quest.Title.Of(quest).AsString()}").Sign(
                new SimpleDealer("user",
                    new Issuer.Of(quest).Value(),
                    new Reward.Of(quest).Value()
                ),
                new SimpleCustomer("user",
                    new Applicant.Of(quest).Value()
                )
            );
        })
        { }
    }
}
