using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Future
{
    public sealed class FkFuture : IFuture
    {
        private readonly Action runAsync;
        private readonly Action<IJob> schedule;

        public FkFuture() : this(
            () => { },
            job => { }
        )
        { }

        public FkFuture(Action runAsync, Action<IJob> schedule)
        {
            this.runAsync = runAsync;
            this.schedule = schedule;
        }

        public void RunAsync()
        {
            this.runAsync();
        }

        public void Schedule(IJob job)
        {
            this.schedule(job);
        }
    }
}
