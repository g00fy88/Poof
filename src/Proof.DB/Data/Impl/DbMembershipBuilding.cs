using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.DB.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms.List;

namespace Poof.Web.Server.Data
{
    public sealed class DbMembershipBuilding : IDataBuilding
    { 
        private readonly ApplicationDbContext context;

        public DbMembershipBuilding(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(string floor)
        {
            this.context.Memberships.Add(
                new DbMembership() { Id = floor }
            );
        }

        public IDataFloor Floor(string id)
        {
            return
                new DbMembershipFloor(this.context,
                    this.context.Memberships.Find(id)
                );
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            return
                new Mapped<DbMembership, string>(entity =>
                    entity.Id,
                    this.context.Memberships.ToList()
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
        }
    }
}
