using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms;

namespace Poof.Talk
{
    public sealed class ClientApi : IApi
    {
        private readonly HttpClient client;

        public ClientApi(HttpClient client)
        {
            this.client = client;
        }

        public ICommand Private(IDemand demand)
        {
            return new CommandOf(this.client, "private", demand);
        }

        public ICommand Public(IDemand demand)
        {
            return new CommandOf(this.client, "public", demand);
        }
    }
}
