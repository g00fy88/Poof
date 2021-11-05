using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms;

namespace Poof.Talk
{
    public interface ICommand
    {
        Task<IOutcome<IInput>> Content();

        Task Touch();
    }
}
