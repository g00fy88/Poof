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
    public sealed class DbFriendshipBuilding : IDataBuilding
    { 
        private readonly ApplicationDbContext context;
        private readonly ICache<DbFriendship> cache;

        public DbFriendshipBuilding(ApplicationDbContext context, ICache<DbFriendship> cache)
        {
            this.context = context;
            this.cache = cache;
        }

        public void Add(string floor)
        {
            this.context.Friendships.Add(
                new DbFriendship() { Id = floor }
            );
            this.context.SaveChanges();
        }

        public IDataFloor Floor(string id)
        {
            return
                new DbFriendshipFloor(
                    this.context,
                    this.cache,
                    id
                );
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            IEnumerable<DbFriendship> result = this.context.Friendships;
            foreach (var match in matches)
            {
                result = new FriendshipMatches(result, match);
            }
            return
                new Mapped<DbFriendship, string>(
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
            this.context.Friendships.Remove(
                this.context.Friendships.Find(floor)
            );
            this.context.SaveChanges();
            this.cache.Clear();
        }
    }
}
