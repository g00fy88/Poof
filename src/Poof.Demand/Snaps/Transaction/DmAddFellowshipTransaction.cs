using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Transaction
{
    public sealed class DmAddFellowshipTransaction : DemandEnvelope
    {
        public DmAddFellowshipTransaction(string fellowship, string type, string id, string title, double amount) : base(() =>
            new DmAddTransactionBase(
                "add-fellowship-transaction",
                "fellowship",
                fellowship,
                type,
                id,
                title,
                amount
            )
        )
        { }
    }
}
