using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Future
{
    public interface IJob
    {
        IIdentity Identity();
        IDemand Demand();
    }
}
