using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.DB;
using Poof.DB.Data;
using Poof.DB.Data.Impl.PropMatch;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms.List;

namespace Poof.Web.Server.Data
{
    public sealed class DbUserBuilding : IDataBuilding
    {
        private readonly ApplicationDbContext context;
        private readonly ICache<ApplicationUser> cache;

        public DbUserBuilding(ApplicationDbContext context, ICache<ApplicationUser> cache)
        {
            this.context = context;
            this.cache = cache;
        }

        public void Add(string floor)
        {
            throw new InvalidOperationException("Users cannot be added over core functionality, but only over registration identity process.");
        }

        public IDataFloor Floor(string id)
        {
            return
                new DbUserFloor(
                    this.context,
                    this.cache,
                    id
                );
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            IEnumerable<ApplicationUser> result = this.context.Users;
            foreach(var match in matches)
            {
                result = new UserMatches(result, match);
            }
            return
                new Mapped<ApplicationUser, string>(
                    entity => entity.Id,
                    result.ToList()
                );
        }

        public IDataBuilding Moved(string name)
        {
            return new DbBuilding(this.context, name);
        }

        public void Remove(string floor)
        {
            this.context.Users.Remove(
                this.context.Users.Find(floor)
            );
            this.context.SaveChanges();
            this.cache.Clear();
        }
    }
}
