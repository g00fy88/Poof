﻿using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.DB.Data;
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

        public DbFellowshipBuilding(ApplicationDbContext context)
        {
            this.context = context;
        }

        public void Add(string floor)
        {
            this.context.Fellowships.Add(
                new DbFellowship() { Id = floor }
            );
        }

        public IDataFloor Floor(string id)
        {
            return
                new DbFellowshipFloor(this.context,
                    this.context.Fellowships.Find(id)
                );
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            return
                new Mapped<DbFellowship, string>(entity =>
                    entity.Id,
                    this.context.Fellowships.ToList()
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
