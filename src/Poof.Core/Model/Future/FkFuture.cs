using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model.Future
{
    public sealed class FkFuture : IFuture
    {
        private readonly Action runAsync;
        private readonly Action<DateTime, IJob> schedule;

        public FkFuture() : this(
            () => { },
            (date, job) => { }
        )
        { }

        public FkFuture(Action runAsync, Action<DateTime, IJob> schedule)
        {
            this.runAsync = runAsync;
            this.schedule = schedule;
        }

        public void RunAsync()
        {
            this.runAsync();
        }

        public void Schedule(DateTime dueDate, IJob job)
        {
            this.schedule(dueDate, job);
        }
    }
}
