﻿using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Transaction
{
    public sealed class DmAddTransaction : DemandEnvelope
    {
        public DmAddTransaction(string user, double amount) : base(() =>
            new PoofDemand("transaction", "configuration", "add-transaction",
                new JSONOf(
                    new JObject(
                        new JProperty("giveside", user),
                        new JProperty("amount", amount)
                    )
                )
            )
        )
        { }
    }
}
