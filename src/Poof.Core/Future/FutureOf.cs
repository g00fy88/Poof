using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Future;
using Poof.Core.Snaps;
using Poof.Snaps;
using Pulse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Poof.Core.Future
{
    /// <summary>
    /// a future that runs async and can be scheduled with jobs to run at a given time.
    /// A new job is only added, if a job with the same identity and the same demand params
    /// was not already added
    /// </summary>
    public sealed class FutureOf : IFuture
    {
        private readonly ISnap<IInput> api;
        private readonly IList<IJob> jobs;

        /// <summary>
        /// a future that runs async and can be scheduled with jobs to run at a given time.
        /// A new job is only added, if a job with the same identity and the same demand params
        /// was not already added
        /// </summary>
        public FutureOf(IDataBuilding mem, IPulse pulse)
        {
            this.api = new FutureSnap(mem, pulse, this);
            this.jobs = new List<IJob>();
        }

        public void RunAsync()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var jobCopy = new List<IJob>();
                    lock (this.jobs)
                    {
                        jobCopy = new List<IJob>(this.jobs);
                    }
                    foreach (var job in jobCopy)
                    {
                        if(job.DueDate() < DateTime.Now)
                        {
                            try
                            {
                                lock (this.jobs)
                                {
                                    this.jobs.Remove(job);
                                }
                                this.api.Convert(job.Demand());
                            }
                            catch
                            {
                                // add logging
                            }
                        }
                    }
                    Task.Delay(1000).Wait();
                }
            });
        }

        public void Schedule(IJob job)
        {
            lock(this.jobs)
            {
                if (
                    new LengthOf(
                        new Filtered<IJob>(j =>
                            new And(
                                new Mapped<string, bool>(p =>
                                    j.Demand().Params().Contains(p) && j.Demand().Param(p, "") == job.Demand().Param(p, ""),
                                    job.Demand().Params()
                                )
                            ).Value(),
                            this.jobs
                        )
                    ).Value() == 0
                )
                {
                    this.jobs.Add(job);
                }
            }
        }
    }
}
