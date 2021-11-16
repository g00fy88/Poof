using Poof.Core.Model.Data;
using Poof.DB.Data;
using Poof.DB.Models;
using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms.List;

namespace Poof.Web.Server.Data
{
    public sealed class DbTransactionBuilding : IDataBuilding
    {
        private readonly ApplicationDbContext context;

        public DbTransactionBuilding(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(string floor)
        {
            this.context.Transactions.Add(
                new DbTransaction() { Id = floor }
            );
            this.context.SaveChanges();
        }

        public IDataFloor Floor(string id)
        {
            return
                new DbTransactionFloor(this.context,
                    this.context.Transactions.Find(id)
                );
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            return
                new Mapped<DbTransaction, string>(entity =>
                    entity.Id,
                    this.context.Transactions.ToList()
                );
        }

        public IDataBuilding Moved(string name)
        {
            return new DbBuilding(this.context, name);
        }

        public void Remove(string floor)
        {
            this.context.Transactions.Remove(
                this.context.Transactions.Find(floor)
            );
            this.context.SaveChanges();
        }
    }
}
