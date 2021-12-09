using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Transaction
{
    public sealed class DmAddTransaction : DemandEnvelope
    {
        public DmAddTransaction(string user, string title, double amount) : base(() =>
            new PoofDemand("transaction", "configuration", "add-transaction",
                new JSONOf(
                    new JObject(
                        new JProperty("taketype", "user"),
                        new JProperty("taketype", "me"),
                        new JProperty("givetype", "user"),
                        new JProperty("giveside", user),
                        new JProperty("title", title),
                        new JProperty("amount", amount)
                    )
                )
            )
        )
        { }

        public DmAddTransaction(string type, string id, string title, double amount) : base(() =>
            new PoofDemand("transaction", "configuration", "add-transaction",
                new JSONOf(
                    new JObject(
                        new JProperty("taketype", "user"),
                        new JProperty("taketype", "me"),
                        new JProperty("givetype", type),
                        new JProperty("giveside", id),
                        new JProperty("title", title),
                        new JProperty("amount", amount)
                    )
                )
            )
        )
        { }

        public DmAddTransaction(string senderType, string senderId, string receiverType, string receiverId, string title, double amount) : base(() =>
            new PoofDemand("transaction", "configuration", "add-transaction",
                new JSONOf(
                    new JObject(
                        new JProperty("taketype", senderType),
                        new JProperty("taketype", senderId),
                        new JProperty("givetype", receiverType),
                        new JProperty("giveside", receiverId),
                        new JProperty("title", title),
                        new JProperty("amount", amount)
                    )
                )
            )
        )
        { }
    }
}
