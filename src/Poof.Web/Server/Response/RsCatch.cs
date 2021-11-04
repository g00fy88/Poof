using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System;
using Yaapii.Atoms;

namespace Poof.Web.Server.Response
{
    /// <summary>
    /// Catches and returns errors.
    /// </summary>
    public sealed class RsCatch<T> : IScalar<IActionResult> where T : Exception
    {
        private readonly int status;
        private readonly Func<T, string> content;
        private readonly IScalar<IActionResult> origin;

        /// <summary>
        /// Catches and returns errors.
        /// </summary>
        public RsCatch(int status, IScalar<IActionResult> origin) : this(
            status,
            ex =>
            {
                var error =
                    new JObject(
                        new JProperty("scope", ""),
                        new JProperty("category", ""),
                        new JProperty("value", ex.Message)
                    );
                if (ex.Data.Contains("errorscope") && ex.Data.Contains("errorcategory") && ex.Data.Contains("errorvalue"))
                {
                    error["scope"] = ex.Data["errorscope"].ToString();
                    error["category"] = ex.Data["errorcategory"].ToString();
                    error["value"] = ex.Data["errorvalue"].ToString();
                }
                return error.ToString();
            },
            origin
        )
        { }

        /// <summary>
        /// Catches and returns errors.
        /// </summary>
        public RsCatch(int status, Func<T, string> content, IScalar<IActionResult> origin)
        {
            this.status = status;
            this.content = content;
            this.origin = origin;
        }

        public IActionResult Value()
        {
            IActionResult result;

            try
            {
                result = this.origin.Value();
            }
            catch (T ex)
            {
                result =
                    new ContentResult()
                    {
                        ContentType = "application/json",
                        Content = this.content(ex),
                        StatusCode = this.status
                    };
            }
            return result;
        }
    }
}
