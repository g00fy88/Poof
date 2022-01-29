using Poof.Core.Model;
using Poof.Core.Model.Future;
using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Future
{
    public sealed class JobOf : IJob
    {
        private readonly DateTime dueDate;
        private readonly IDemand demand;

        public JobOf(IDemand demand) : this(
            DateTime.Now,
            demand
        )
        { }

        public JobOf(DateTime dueDate, IDemand demand)
        {
            this.dueDate = dueDate;
            this.demand = demand;
        }

        public IDemand Demand()
        {
            return this.demand;
        }

        public DateTime DueDate()
        {
            return this.dueDate;
        }
    }
}
