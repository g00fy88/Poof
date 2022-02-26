using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms;

namespace Poof.Talk
{
    public sealed class SimpleCommand : ICommand
    {
        private readonly IOutcome<IInput> content;

        public SimpleCommand(IOutcome<IInput> content)
        {
            this.content = content;
        }

        public async Task<IOutcome<IInput>> Content()
        {
            return this.content;
        }

        public async Task Touch()
        { }
    }
}
