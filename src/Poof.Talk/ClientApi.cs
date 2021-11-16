using Poof.Snaps;
using System.Net.Http;

namespace Poof.Talk
{
    public sealed class ClientApi : IApi
    {
        private readonly HttpClient client;

        public ClientApi(HttpClient client)
        {
            this.client = client;
        }

        public ICommand Private(IDemand demand, string contentType)
        {
            return new CommandOf(this.client, "private", demand, contentType);
        }

        public ICommand Public(IDemand demand, string contentType)
        {
            return new CommandOf(this.client, "public", demand, contentType);
        }
    }
}
