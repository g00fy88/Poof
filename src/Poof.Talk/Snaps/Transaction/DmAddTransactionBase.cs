using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Transaction
{
    public sealed class DmAddTransactionBase : DemandEnvelope
    {
        public DmAddTransactionBase(string action, string taketype, string takeside, string givetype, string giveside, string title, double amount) : base(() =>
            new PoofDemand("transaction", "configuration", action,
                new JSONOf(
                    new JObject(
                        new JProperty("taketype", taketype),
                        new JProperty("takeside", takeside),
                        new JProperty("givetype", givetype),
                        new JProperty("giveside", giveside),
                        new JProperty("title", title),
                        new JProperty("amount", amount)
                    )
                )
            )
        )
        { }
    }
}
