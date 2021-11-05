using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Poof.DB.Test
{
    public sealed class TestMembershipFloor : IDataFloor
    {
        private readonly DbMembership entity;
        private readonly IList<ApplicationUser> users;
        private readonly IList<DbFellowship> fellowships;

        public TestMembershipFloor(DbMembership entity, IList<ApplicationUser> users, IList<DbFellowship> fellowships)
        {
            this.entity = entity;
            this.users = users;
            this.fellowships = fellowships;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbMembership, T>(this.entity).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<DbMembership, T>(
                this.entity, 
                name,
                id => new FirstOf<ApplicationUser>(
                    user => user.Id == id, 
                    this.users,
                    new InvalidOperationException($"Unable to add owner '{id}', because the user does not exist.")
                ).Value(),
                id => new FirstOf<DbFellowship>(
                    fellowship => fellowship.Id == id, 
                    this.fellowships,
                    new InvalidOperationException($"Unable to add team '{id}', because the fellowship does not exist.")
                ).Value()
            ).Invoke(value);
        }
    }
}
