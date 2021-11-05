using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.DB.Test
{
    public sealed class TestUserFloor : IDataFloor
    {
        private readonly ApplicationUser entity;

        public TestUserFloor(ApplicationUser entity)
        {
            this.entity = entity;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<ApplicationUser, T>(this.entity).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<ApplicationUser, T>(
                this.entity,
                name,
                id => new ApplicationUser() { Id = id },
                id => new DbFellowship() { Id = id }
            ).Invoke(value);
        }
    }
}
