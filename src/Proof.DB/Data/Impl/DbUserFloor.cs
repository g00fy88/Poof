using Poof.Core.Model.Data;
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
        private readonly ApplicationUser user;

        public DbUserFloor(ApplicationDbContext context, ApplicationUser user)
        {
            this.context = context;
            this.user = user;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<ApplicationUser, T>(this.user).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<ApplicationUser, T>(this.user, name, this.context).Invoke(value);

            this.context.Users.Update(this.user);
            this.context.SaveChanges();
        }
    }
}
