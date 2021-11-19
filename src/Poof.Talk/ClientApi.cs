using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Yaapii.Atoms;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.JSON;

namespace Poof.Talk
{
    public sealed class ClientApi : IApi
    {
        private readonly HttpClient client;
        private readonly IDictionary<string, Func<Task>> actions;
        private readonly IScalar<bool> statusSensor;

        public ClientApi(HttpClient client)
        {
            this.client = client;
            this.actions = new Dictionary<string, Func<Task>>();
            this.statusSensor =
                new ScalarOf<bool>(() =>
                {
                    Task.Run(async () =>
                    {
                        while (true)
                        {
                            var stati = await Status();
                            foreach(var name in this.actions.Keys)
                            {
                                if(stati.Contains(name))
                                {
                                    await this.actions[name]();
                                }
                            }
                            await Task.Delay(2000);
                        }
                    });
                    return true;
                });
        }

        public ICommand Private(IDemand demand, string contentType)
        {
            return new CommandOf(this.client, "private", demand, contentType);
        }

        public ICommand Public(IDemand demand, string contentType)
        {
            return new CommandOf(this.client, "public", demand, contentType);
        }

        public void AddStatusAction(string name, Func<Task> action)
        {
            if(this.statusSensor.Value())
            {
                this.actions[name] = action;
            }
        }

        private async Task<IList<string>> Status()
        {
            var response = await this.client.GetAsync("user/status");
            return
                new JSONOf(
                    new InputOf(
                        await response.Content.ReadAsByteArrayAsync()
                    )
                ).Values("[*]");
        }
    }
}
