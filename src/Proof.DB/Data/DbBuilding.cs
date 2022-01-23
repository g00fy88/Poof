using Poof.Core.Model.Data;
using Poof.DB.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using Yaapii.Atoms.Map;

namespace Poof.Web.Server.Data
{
    public class DbBuilding : IDataBuilding
    {
        private readonly string name;
        private readonly IDictionary<string, IDataBuilding> buildings;

        public DbBuilding(ApplicationDbContext context) : this(
            context,
            ""
        )
        { }

        public DbBuilding(ApplicationDbContext context, string name) : this(
            name,
            new MapOf<IDataBuilding>(
                new KvpOf<IDataBuilding>("user", () =>
                    new DbUserBuilding(context, 
                        new DbCache<ApplicationUser>(context.Users)
                    )
                ),
                new KvpOf<IDataBuilding>("fellowship", () =>
                    new DbFellowshipBuilding(context, 
                        new DbCache<DbFellowship>(context.Fellowships)
                    )
                ),
                new KvpOf<IDataBuilding>("transaction", () =>
                    new DbTransactionBuilding(context,
                        new DbCache<DbTransaction>(context.Transactions)
                    )
                ),
                new KvpOf<IDataBuilding>("membership", () =>
                    new DbMembershipBuilding(context,
                        new DbCache<DbMembership>(context.Memberships)
                    )
                ),
                new KvpOf<IDataBuilding>("quest", () =>
                    new DbQuestBuilding(context,
                        new DbCache<DbQuest>(context.Quests)
                    )
                )
            )
        )
        { }

        private DbBuilding(string name, IDictionary<string, IDataBuilding> buildings)
        {
            this.name = name;
            this.buildings = buildings;
        }

        public void Add(string floor)
        {
            if (!this.buildings.ContainsKey(this.name))
            {
                throw new InvalidOperationException($"The data table '{name}' is unknown.");
            }
            this.buildings[name].Add(floor);
        }

        public IDataFloor Floor(string id)
        {
            if (!this.buildings.ContainsKey(this.name))
            {
                throw new InvalidOperationException($"The data table '{name}' is unknown.");
            }
            return this.buildings[name].Floor(id);
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            if (!this.buildings.ContainsKey(this.name))
            {
                throw new InvalidOperationException($"The data table '{name}' is unknown.");
            }
            return this.buildings[name].Floors(matches);
        }

        public IDataBuilding Moved(string name)
        {
            if(!this.buildings.ContainsKey(name))
            {
                throw new InvalidOperationException($"The data table '{name}' is unknown.");
            }
            return new DbBuilding(name, this.buildings);
        }

        public void Remove(string floor)
        {
            if (!this.buildings.ContainsKey(this.name))
            {
                throw new InvalidOperationException($"The data table '{name}' is unknown.");
            }
            this.buildings[name].Remove(floor);
        }
    }
}
