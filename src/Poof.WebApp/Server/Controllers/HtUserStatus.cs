using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading;
using System.Threading.Tasks;
using Poof.Core.Model;
using Poof.Core.Snaps;
using Poof.Web.Server.Request;
using Poof.Web.Server.Response;
using Yaapii.Atoms;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Microsoft.AspNetCore.Authorization;
using Poof.WebApp.Server.UserStatus;
using System.Text;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Poof.Web.Server.Controllers
{
    [Authorize]
    [ApiController]
    [Route("user/status")]
    public class HtUserStatus : ControllerBase
    {
        private readonly IIdentityStatus status;

        public HtUserStatus(IIdentityStatus status)
        {
            this.status = status;
        }

        [HttpGet]
        public async Task<IActionResult> Invoke(CancellationToken token)
        {
            return await
               Task.Run(() =>
               {
                   var user = new HttpIdentity(this.HttpContext).UserID();
                   var stati = new List<string>(this.status.ChangedStati(user));

                   IActionResult result =
                       new ContentResult()
                       {
                           StatusCode = 200,
                           Content = new JArray(stati).ToString(),
                           ContentType = $"application/json; charset={Encoding.UTF8.WebName}"
                       };

                   foreach(var name in stati)
                   {
                       this.status.Remove(user, name);
                   }

                   return result;
               },
               token);
        }
    }
}
