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
        private readonly IIdentity identity;
        private readonly IDemand demand;

        public JobOf(string user, IDemand demand) : this(
            new UserIdentity(user),
            demand
        )
        { }

        public JobOf(IIdentity identity, IDemand demand)
        {
            this.identity = identity;
            this.demand = demand;
        }

        public IDemand Demand()
        {
            return this.demand;
        }

        public IIdentity Identity()
        {
            return this.identity;
        }
    }
}
