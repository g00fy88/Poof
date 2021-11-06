using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Poof.Core.Model;
using Poof.Core.Snaps;
using Poof.Snaps;
using Poof.Web.Server.Request;
using Poof.Web.Server.Response;
using Yaapii.Atoms;
using Poof.Core.Model.Data;

namespace Poof.Web.Server.Controllers
{
    [ApiController]
    [Route("public/{entity}/{category}/{act}")]
    public class HtPublic : ControllerBase
    {
        private readonly ISnap<IInput> snap;

        public HtPublic(IDataBuilding memory)
        {
            this.snap = new PublicSnap(memory);
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Invoke(IFormFile file, CancellationToken token)
        {
            return await
               Task.Run(() =>
               {
                   IActionResult result = new ContentResult() { StatusCode = 200 };

                   var demand =
                           new EmptyDemand()
                               .Refined("entity", Param("entity"))
                               .Refined("category", Param("category"))
                               .Refined("action", Param("act"));
                   foreach (var param in new QueryParams(this.Request))
                   {
                       demand.Refined(param.Key, param.Value);
                   }

                   return
                       new RsCatch<ArgumentException>(400,
                            new RsCatch<InvalidOperationException>(400,
                                new RsCatch<UnauthorizedAccessException>(401,
                                    new RsApi(
                                        this.snap,
                                        demand
                                    )
                                )
                            )
                        ).Value();
               },
               token);
        }

        private string Param(string name)
        {
            return new RouteParam(name, this).Value();
        }
    }
}
