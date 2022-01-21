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
        private readonly Func<IIdentity, ISnap<IInput>> api;
        private readonly IList<IKvp<DateTime, IJob>> jobs;

        /// <summary>
        /// a future that runs async and can be scheduled with jobs to run at a given time.
        /// A new job is only added, if a job with the same identity and the same demand params
        /// was not already added
        /// </summary>
        public FutureOf(IDataBuilding mem, IPulse pulse)
        {
            this.api = identity => new PrivateSnap(mem, pulse, identity, this);
            this.jobs = new List<IKvp<DateTime, IJob>>();
        }

        public void RunAsync()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    var jobCopy = new List<IKvp<DateTime, IJob>>();
                    lock (this.jobs)
                    {
                        jobCopy = new List<IKvp<DateTime, IJob>>(this.jobs);
                    }
                    foreach (var job in jobCopy)
                    {
                        if(job.Key() < DateTime.Now)
                        {
                            try
                            {
                                lock (this.jobs)
                                {
                                    this.jobs.Remove(job);
                                }
                                this.api(job.Value().Identity()).Convert(job.Value().Demand());
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

        public void Schedule(DateTime dueDate, IJob job)
        {
            lock(this.jobs)
            {
                if (
                    new LengthOf(
                        new Filtered<IJob>(j =>
                            j.Identity().UserID() == job.Identity().UserID() &&
                            new And(
                                new Mapped<string, bool>(p =>
                                    j.Demand().Params().Contains(p) && j.Demand().Param(p, "") == job.Demand().Param(p, ""),
                                    job.Demand().Params()
                                )
                            ).Value(),
                            new Mapped<IKvp<DateTime, IJob>, IJob>(kv =>
                                kv.Value(),
                                this.jobs
                            )
                        )
                    ).Value() == 0
                )
                {
                    this.jobs.Add(
                        new KvpOf<DateTime, IJob>(dueDate, job)
                    );
                }
            }
        }
    }
}
