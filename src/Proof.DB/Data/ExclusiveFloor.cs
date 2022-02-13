using Microsoft.EntityFrameworkCore;
using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poof.DB.Data
{
    public sealed class ExclusiveFloor<TProp> : IDataFloor where TProp : class
    {
        private readonly DbSet<TProp> mutex;
        private readonly IDataFloor origin;

        public ExclusiveFloor(DbSet<TProp> mutex, IDataFloor origin)
        {
            this.mutex = mutex;
            this.origin = origin;
        }

        public T Prop<T>(string name)
        {
            return this.origin.Prop<T>(name);
        }

        public void Update<T>(string name, T value)
        {
            lock(this.mutex)
            {
                this.origin.Update(name, value);
            }
        }
    }
}
