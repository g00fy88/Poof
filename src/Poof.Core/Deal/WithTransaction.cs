using Poof.Core.Entity.Transaction;
using Poof.Core.Model.Data;
using Poof.Core.Model.Deal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Deal
{
    public sealed class WithTransaction : IDeal
    {
        private readonly string title;
        private readonly IDataBuilding mem;
        private readonly IDeal origin;

        public WithTransaction(string title, IDataBuilding mem, IDeal origin)
        {
            this.title = title;
            this.mem = mem;
            this.origin = origin;
        }

        public void Sign(IDealer sender, ICustomer receiver)
        {
            this.origin.Sign(sender, receiver);

            if (sender.ID() != receiver.ID())
            {
                new TransactionOf(mem, new Transactions(mem).New()).Update(
                    new Title(this.title),
                    new Entity.Type("main"),
                    new TakeSide(sender.Type(), sender.ID()),
                    new GiveSide(receiver.Type(), receiver.ID()),
                    new Date(DateTime.Now),
                    new Amount(sender.Points())
                );
            }
        }
    }
}
