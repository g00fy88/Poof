using Microsoft.AspNetCore.Authorization;
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
using Yaapii.Atoms.IO;
using Poof.Core.Model.Data;
using Pulse;
using Poof.Core.Model.Future;

namespace Poof.Web.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("private/{entity}/{category}/{act}")]
    public class HtPrivate : ControllerBase
    {
        private readonly IDataBuilding memory;
        private readonly IPulse pulse;
        private readonly IFuture future;

        public HtPrivate(IDataBuilding memory, IPulse pulse, IFuture future)
        {
            this.memory = memory;
            this.pulse = pulse;
            this.future = future;
        }

        [HttpGet]
        [HttpPost]
        public async Task<IActionResult> Invoke(CancellationToken token)
        {
            return await
               Task.Run(() =>
               {
                   IActionResult result = new ContentResult() { StatusCode = 200 };
                   var payload =
                          new InputOf(
                              new StrictBody(this.Request,
                                  new RequestBodyBytes(this)
                              ).AsBytes()
                          );

                   var demand =
                           new DemandOf(payload)
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
                                        new PrivateSnap(this.memory, this.pulse, new HttpIdentity(this.HttpContext), this.future),
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
