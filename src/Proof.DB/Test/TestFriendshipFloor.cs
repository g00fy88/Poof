using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Poof.DB.Test
{
    public sealed class TestFriendshipFloor : IDataFloor
    {
        private readonly DbFriendship entity;
        private readonly IList<ApplicationUser> users;

        public TestFriendshipFloor(DbFriendship entity, IList<ApplicationUser> users)
        {
            this.entity = entity;
            this.users = users;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbFriendship, T>(this.entity).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<DbFriendship, T>(
                this.entity, 
                name,
                id => new FirstOf<ApplicationUser>(
                    user => user.Id == id, 
                    this.users,
                    new InvalidOperationException($"Unable to add owner '{id}', because the user does not exist.")
                ).Value(),
                id => new DbFellowship() { Id = id }
            ).Invoke(value);
        }
    }
}
