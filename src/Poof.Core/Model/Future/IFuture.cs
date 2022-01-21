using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Future
{
    public interface IFuture
    {
        void RunAsync();
        void Schedule(DateTime dueDate, IJob job);
    }
}
