using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Map;

namespace Poof.DB.Data
{
    public sealed class DbQuestFloor : IDataFloor
    {
        private readonly ApplicationDbContext context;
        private readonly DbQuest quest;

        public DbQuestFloor(ApplicationDbContext context, DbQuest quest)
        {
            this.context = context;
            this.quest = quest;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbQuest, T>(this.quest).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<DbQuest, T>(this.quest, name, this.context).Invoke(value);

            this.context.Quests.Update(this.quest);
            this.context.SaveChanges();
        }
    }
}
