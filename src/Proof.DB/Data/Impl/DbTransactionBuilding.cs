using Poof.Core.Model.Data;
using Poof.DB;
using Poof.DB.Data;
using Poof.DB.Data.Impl.PropMatch;
using Poof.DB.Models;
using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms.List;

namespace Poof.Web.Server.Data
{
    public sealed class DbTransactionBuilding : IDataBuilding
    {
        private readonly ApplicationDbContext context;
        private readonly ICache<DbTransaction> cache;

        public DbTransactionBuilding(ApplicationDbContext context, ICache<DbTransaction> cache)
        {
            this.context = context;
            this.cache = cache;
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
                new DbTransactionFloor(
                    this.context,
                    this.cache,
                    id
                );
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            IEnumerable<DbTransaction> result = this.context.Transactions;
            foreach (var match in matches)
            {
                result = new TransactionMatches(result, match);
            }
            return
                new Mapped<DbTransaction, string>(entity =>
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
            this.context.Transactions.Remove(
                this.context.Transactions.Find(floor)
            );
            this.context.SaveChanges();
            this.cache.Clear();
        }
    }
}
