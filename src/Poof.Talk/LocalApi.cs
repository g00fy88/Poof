using Newtonsoft.Json.Linq;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

namespace Poof.Talk
{
    public sealed class LocalApi : IApi
    {
        private readonly IScalar<IInput> response;

        public LocalApi(JToken json) : this(
            new ScalarOf<IInput>(()=>
                new InputOf(json.ToString())
            )
        )
        { }

        public LocalApi(IScalar<IInput> response)
        {
            this.response = response;
        }

        public void AddStatusAction(string name, Func<Task> action)
        { }

        public ICommand Private(IDemand demand, string contentType)
        {
            return new SimpleCommand(new OutcomeOf<IInput>(this.response.Value()));
        }

        public ICommand Public(IDemand demand, string contentType)
        {
            return new SimpleCommand(new OutcomeOf<IInput>(this.response.Value()));
        }
    }
}
