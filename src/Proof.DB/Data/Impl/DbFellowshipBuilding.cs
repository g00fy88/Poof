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
    public sealed class DbFellowshipBuilding : IDataBuilding
    {
        private readonly ApplicationDbContext context;
        private readonly ICache<DbFellowship> cache;

        public DbFellowshipBuilding(ApplicationDbContext context, ICache<DbFellowship> cache)
        {
            this.context = context;
            this.cache = cache;
        }

        public void Add(string floor)
        {
            this.context.Fellowships.Add(
                new DbFellowship() { Id = floor }
            );
            this.context.SaveChanges();
        }

        public IDataFloor Floor(string id)
        {
            return
                new DbFellowshipFloor(
                    this.context,
                    this.cache,
                    id
                );
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            IEnumerable<DbFellowship> result = this.context.Fellowships;
            foreach (var match in matches)
            {
                result = new FellowshipMatches(result, match);
            }
            return
                new Mapped<DbFellowship, string>(
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
            this.context.Fellowships.Remove(
                this.context.Fellowships.Find(floor)
            );
            this.context.SaveChanges();
            this.cache.Clear();
        }
    }
}
