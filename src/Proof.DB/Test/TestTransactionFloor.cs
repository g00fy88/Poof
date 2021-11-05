﻿using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.DB.Test
{
    public sealed class TestTransactionFloor : IDataFloor
    {
        private readonly DbTransaction entity;

        public TestTransactionFloor(DbTransaction entity)
        {
            this.entity = entity;
        }

        public T Prop<T>(string name)
        {
            return new DbValue<DbTransaction, T>(this.entity).Invoke(name);
        }

        public void Update<T>(string name, T value)
        {
            new DbUpdate<DbTransaction, T>(
                this.entity,
                name,
                id => new ApplicationUser() { Id = id },
                id => new DbFellowship() { Id = id }
            ).Invoke(value);
        }
    }
}
