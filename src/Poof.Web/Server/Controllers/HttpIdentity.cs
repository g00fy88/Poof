using Microsoft.AspNetCore.Http;
using Poof.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Yaapii.Atoms.Scalar;

namespace Poof.Web.Server.Controllers
{
    public class HttpIdentity : IIdentity
    {
        private ScalarOf<string> user;

        public HttpIdentity(HttpContext context)
        {
            this.user =
                new ScalarOf<string>(() =>
                    new FirstOf<Claim>(
                        claim => claim.Type.EndsWith("nameidentifier"),
                        context.User.Claims,
                        new InvalidOperationException("Unable to retrieve user, because it was not found in the http context.")
                    ).Value().Value
                );
        }

        public string UserID()
        {
            return user.Value();
        }
    }
}
