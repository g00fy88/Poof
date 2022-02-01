using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Deal
{
    public interface IDeal
    {
        void Sign(IDealer sender, ICustomer receiver);
    }
}
