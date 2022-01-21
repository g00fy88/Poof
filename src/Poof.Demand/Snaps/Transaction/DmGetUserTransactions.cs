using Poof.Snaps.Demand;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Snaps.Transaction
{
    public sealed class DmGetUserTransactions : DemandEnvelope
    {
        public DmGetUserTransactions() : base(()=>
            new PoofDemand("transaction", "discovery", "get-user-transactions")
        )
        { }
    }
}
