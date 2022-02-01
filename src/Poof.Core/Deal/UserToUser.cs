using Poof.Core.Entity.User;
using Poof.Core.Model.Data;
using Poof.Core.Model.Deal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Deal
{
    public class UserToUser : IDeal
    {
        private readonly IDataBuilding mem;

        public UserToUser(IDataBuilding mem)
        {
            this.mem = mem;
        }

        public void Sign(IDealer sender, ICustomer receiver)
        {
            var takeUser = new UserOf(mem, sender.ID());
            var giveUser = new UserOf(mem, receiver.ID());
            var amount = sender.Points();

            takeUser.Update(new Points(-amount));
            giveUser.Update(
                new Points(amount),
                new BalanceScore(
                    amount * 
                    (new Points.TakeFactor(takeUser).Value() + new Points.GiveFactor(giveUser).Value())
                )
            );
        }
    }
}
