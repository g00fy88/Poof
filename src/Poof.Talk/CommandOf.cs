using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.List;

namespace Poof.Talk
{
    public sealed class CommandOf : ICommand
    {
        private readonly HttpClient client;
        private readonly string kind;
        private readonly IDemand demand;
        private readonly string contentType;

        public CommandOf(HttpClient client, string kind, IDemand demand, string contentType)
        {
            this.client = client;
            this.kind = kind;
            this.demand = demand;
            this.contentType = contentType;
        }

        public async Task<IOutcome<IInput>> Content()
        {
            var uri = DemandUri();
            var response = await Response(uri);

            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"Unable to retrieve outcome from request '{uri}', " +
                    $"because it returned an error. Status-Code: {response.StatusCode}");
            }

            return
                new OutcomeOf<IInput>(
                    new InputOf(
                        await response.Content.ReadAsByteArrayAsync()
                    )
                );
        }

        public async Task Touch()
        {
            var uri = DemandUri();
            var response = await Response(uri);
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                throw new InvalidOperationException($"Unable to request '{uri}', " +
                    $"because it returned an error. Status-Code: {response.StatusCode}");
            }
        }

        private Task<HttpResponseMessage> Response(string uri)
        {
            var content =
                new ByteArrayContent(
                    new BytesOf(this.demand.Body()).AsBytes()
                );
            //content.Headers.ContentType.MediaType = this.contentType;
            return this.client.PostAsync(uri, content);
        }
        
        private string DemandUri()
        {
            var knownParams = new ListOf<string>("entity", "category", "action");
            var uri=
                $"{this.kind}/" +
                $"{this.demand.Param("entity")}/" +
                $"{this.demand.Param("category")}/" +
                $"{this.demand.Param("action")}?";

            var queries = new List<string>();
            foreach(var key in this.demand.Params())
            {
                if(!knownParams.Contains(key))
                {
                    queries.Add($"key={demand.Param(key)}");
                }
            }

            uri += string.Join('&', queries);

            return uri;
        }
    }
}
