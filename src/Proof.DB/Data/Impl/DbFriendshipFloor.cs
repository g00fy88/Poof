using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Map;

namespace Poof.DB.Data
{
    public sealed class DbFriendshipFloor : IDataFloor
    {
        private readonly ApplicationDbContext context;
        private readonly ICache<DbFriendship> cache;
        private readonly string id;

        public DbFriendshipFloor(ApplicationDbContext context, ICache<DbFriendship> cache, string id)
        {
            this.context = context;
            this.cache = cache;
            this.id = id;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbFriendship, T>(this.cache.Value(this.id)).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            lock (this.id)
            {
                this.context.Friendships.Update(
                    new DbUpdate<DbFriendship, T>(
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
