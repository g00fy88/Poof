using Microsoft.EntityFrameworkCore;
using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poof.DB.Data
{
    public sealed class ExclusiveBuilding<T> : IDataBuilding where T : class
    {
        private readonly DbSet<T> mutex;
        private readonly IDataBuilding origin;

        public ExclusiveBuilding(DbSet<T> mutex, IDataBuilding origin)
        {
            this.mutex = mutex;
            this.origin = origin;
        }

        public void Add(string floor)
        {
            lock(this.mutex)
            {
                this.origin.Add(floor);
            }
        }

        public IDataFloor Floor(string id)
        {
            return new ExclusiveFloor<T>(this.mutex, this.origin.Floor(id))
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            lock(this.mutex)
            {
                return this.origin.Floors(matches);
            }
        }

        public IDataBuilding Moved(string name)
        {
            return this.origin.Moved(name);
        }

        public void Remove(string floor)
        {
            lock(this.mutex)
            {
                this.origin.Remove(floor);
            }
        }
    }
}
