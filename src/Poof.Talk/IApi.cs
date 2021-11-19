using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Poof.Talk
{
    public interface IApi
    {
        ICommand Public(IDemand demand, string contentType);

        ICommand Private(IDemand demand, string contentType);

        void AddStatusAction(string name, Func<Task> action);
    }
}
