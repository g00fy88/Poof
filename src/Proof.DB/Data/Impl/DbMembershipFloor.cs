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
        private readonly DbMembership membership;

        public DbMembershipFloor(ApplicationDbContext context, DbMembership membership)
        {
            this.context = context;
            this.membership = membership;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbMembership, T>(this.membership).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<DbMembership, T>(this.membership, name, this.context).Invoke(value);

            this.context.Memberships.Update(this.membership);
            this.context.SaveChanges();
        }
    }
}
