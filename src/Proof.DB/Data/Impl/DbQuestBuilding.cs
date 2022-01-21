using Poof.Core.Model.Data;
using Poof.DB.Data;
using Poof.DB.Data.Impl.PropMatch;
using Poof.DB.Models;
using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms.List;

namespace Poof.Web.Server.Data
{
    public sealed class DbQuestBuilding : IDataBuilding
    {
        private readonly ApplicationDbContext context;

        public DbQuestBuilding(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(string floor)
        {
            this.context.Quests.Add(
                new DbQuest() { Id = floor }
            );
            this.context.SaveChanges();
        }

        public IDataFloor Floor(string id)
        {
            return
                new DbQuestFloor(this.context,
                    this.context.Quests.Find(id)
                );
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            IEnumerable<DbQuest> result = this.context.Quests;
            foreach (var match in matches)
            {
                result = new QuestMatches(result, match);
            }
            return
                new Mapped<DbQuest, string>(entity =>
                    entity.Id,
                    result.ToList()
                );
        }

        public IDataBuilding Moved(string name)
        {
            return new DbBuilding(this.context, name);
        }

        public void Remove(string floor)
        {
            this.context.Quests.Remove(
                this.context.Quests.Find(floor)
            );
            this.context.SaveChanges();
        }
    }
}
