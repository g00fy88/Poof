using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk
{
    public interface IApi
    {
        ICommand Public(IDemand demand, string contentType);

        ICommand Private(IDemand demand, string contentType);
    }
}
