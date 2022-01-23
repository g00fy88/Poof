using Poof.Core.Model.Data;
using Poof.DB;
using Poof.DB.Data;
using Poof.DB.Models;
using System;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Map;

namespace Poof.Web.Server.Data
{
    public sealed class DbUserFloor : IDataFloor
    {
        private readonly ApplicationDbContext context;
        private readonly ICache<ApplicationUser> cache;
        private readonly string id;

        public DbUserFloor(ApplicationDbContext context, ICache<ApplicationUser> cache, string id)
        {
            this.context = context;
            this.cache = cache;
            this.id = id;
        }

        public T Prop<T>(string name)
        {
            return
                new DbValue<ApplicationUser, T>(
                    this.cache.Value(this.id)
                ).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            lock (this.id)
            {
                this.context.Users.Update(
                    new DbUpdate<ApplicationUser, T>(
                        this.cache.Value(this.id),
                        name,
                        this.context
                    ).Invoke(value)
                );
                this.context.SaveChanges();
            }
            this.cache.Clear();
        }
    }
}
