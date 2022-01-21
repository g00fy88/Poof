using Newtonsoft.Json.Linq;
using Poof.Snaps.Demand;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Transaction
{
    public sealed class DmAddUserTransaction : DemandEnvelope
    {
        public DmAddUserTransaction(string user, string title, double amount) : this(
            "user",
            user,
            title,
            amount
        )
        { }

        public DmAddUserTransaction(string type, string id, string title, double amount) : base(() =>
            new DmAddTransactionBase(
                "add-user-transaction",
                "user",
                "me",
                type,
                id,
                title,
                amount
            )
        )
        { }
    }
}
