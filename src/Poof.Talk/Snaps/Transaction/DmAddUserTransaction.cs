using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Transaction
{
    public sealed class DmAddUserTransaction : DemandEnvelope
    {
        public DmAddUserTransaction(string user, string title, double amount) : base(() =>
            new PoofDemand("transaction", "configuration", "add-user-transaction",
                new JSONOf(
                    new JObject(
                        new JProperty("taketype", "user"),
                        new JProperty("takeside", "me"),
                        new JProperty("givetype", "user"),
                        new JProperty("giveside", user),
                        new JProperty("title", title),
                        new JProperty("amount", amount)
                    )
                )
            )
        )
        { }

        public DmAddUserTransaction(string type, string id, string title, double amount) : base(() =>
            new PoofDemand("transaction", "configuration", "add-user-transaction",
                new JSONOf(
                    new JObject(
                        new JProperty("taketype", "user"),
                        new JProperty("takeside", "me"),
                        new JProperty("givetype", type),
                        new JProperty("giveside", id),
                        new JProperty("title", title),
                        new JProperty("amount", amount)
                    )
                )
            )
        )
        { }
    }
}
