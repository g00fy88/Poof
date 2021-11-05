using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Map;

namespace Poof.DB.Data
{
    public sealed class DbFellowshipFloor : IDataFloor
    {
        private readonly ApplicationDbContext context;
        private readonly DbFellowship fellowship;

        public DbFellowshipFloor(ApplicationDbContext context, DbFellowship fellowship)
        {
            this.context = context;
            this.fellowship = fellowship;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbFellowship, T>(this.fellowship).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<DbFellowship, T>(this.fellowship, name, this.context).Invoke(value);

            this.context.Fellowships.Update(this.fellowship);
            this.context.SaveChanges();
        }
    }
}
