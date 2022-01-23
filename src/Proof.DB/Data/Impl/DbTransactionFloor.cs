using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Map;

namespace Poof.DB.Data
{
    public sealed class DbTransactionFloor : IDataFloor
    {
        private readonly ApplicationDbContext context;
        private readonly ICache<DbTransaction> cache;
        private readonly string id;

        public DbTransactionFloor(ApplicationDbContext context, ICache<DbTransaction> cache, string id)
        {
            this.context = context;
            this.cache = cache;
            this.id = id;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbTransaction, T>(this.cache.Value(this.id)).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            lock (this.id)
            {
                this.context.Transactions.Update(
                    new DbUpdate<DbTransaction, T>(
                        this.cache.Value(this.id),
                        name,
                        this.context
                    ).Invoke(value)
                );
                this.context.SaveChanges();
                this.cache.Clear();
            }
        }
    }
}
