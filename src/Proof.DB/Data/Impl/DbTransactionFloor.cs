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
        private readonly DbTransaction transaction;

        public DbTransactionFloor(ApplicationDbContext context, DbTransaction transaction)
        {
            this.context = context;
            this.transaction = transaction;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbTransaction, T>(this.transaction).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<DbTransaction, T>(this.transaction, name).Invoke(value);

            this.context.Transactions.Update(this.transaction);
            this.context.SaveChanges();
        }
    }
}
