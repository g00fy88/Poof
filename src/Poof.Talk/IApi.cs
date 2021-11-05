using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk
{
    public interface IApi
    {
        ICommand Public(IDemand demand);

        ICommand Private(IDemand demand);
    }
}
