using Poof.Core.Model.Data;
using Poof.Core.Model.Deal;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Map;

namespace Poof.Core.Deal
{
    public sealed class PoofDeal : IDeal
    {
        private readonly IDictionary<string, IDeal> map;

        public PoofDeal(IDataBuilding mem)
        {
            this.map =
                new MapOf<IDeal>(
                    new KvpOf<IDeal>("user-user", new UserToUser(mem))
                );
        }

        public void Sign(IDealer sender, ICustomer receiver)
        {
            var dealType = $"{sender.Type()}-{receiver.Type()}";
            if (!map.ContainsKey(dealType))
            {
                throw new InvalidOperationException($"Unable to perform deal, because a deal from '{sender.Type()}' to '{receiver.Type()}' is not supported");
            }
            this.map[dealType].Sign(sender, receiver);
        }
    }
}
