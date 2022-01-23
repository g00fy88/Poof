using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Map;

namespace Poof.DB.Data
{
    public sealed class DbMembershipFloor : IDataFloor
    {
        private readonly ApplicationDbContext context;
        private readonly ICache<DbMembership> cache;
        private readonly string id;

        public DbMembershipFloor(ApplicationDbContext context, ICache<DbMembership> cache, string id)
        {
            this.context = context;
            this.cache = cache;
            this.id = id;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbMembership, T>(this.cache.Value(this.id)).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            lock (this.id)
            {
                this.context.Memberships.Update(
                    new DbUpdate<DbMembership, T>(
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
