using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.DB.Test
{
    public sealed class TestFellowshipFloor : IDataFloor
    {
        private readonly DbFellowship entity;

        public TestFellowshipFloor(DbFellowship entity)
        {
            this.entity = entity;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbFellowship, T>(this.entity).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<DbFellowship, T>(this.entity, name).Invoke(value);
        }
    }
}
