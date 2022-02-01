using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Deal
{
    public interface IDealer
    {
        string Type();
        string ID();
        double Points();
    }
}
