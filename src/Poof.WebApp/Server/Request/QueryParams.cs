using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yaapii.Atoms;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Poof.Web.Server.Request
{
    /// <summary>
    /// The query params of the request as string map
    /// </summary>
    public sealed class QueryParams : MapEnvelope
    {
        /// <summary>
        /// The query params of the request as string map
        /// </summary>
        public QueryParams(Controller controller) : this(
            new Live<IQueryCollection>(() => controller.HttpContext.Request.Query)
        )
        { }

        /// <summary>
        /// The query params of the request as string map
        /// </summary>
        public QueryParams(HttpRequest request) : this(
            new Live<IQueryCollection>(() => request.Query)
        )
        { }

        /// <summary>
        /// The query params of the request as string map
        /// </summary>
        public QueryParams(IQueryCollection query) : this(
            new Live<IQueryCollection>(query)
        )
        { }

        /// <summary>
        /// The query params of the request as string map
        /// </summary>
        public QueryParams(IScalar<IQueryCollection> query) : base(() =>
        {
            var map = new Dictionary<string, string>();
            var source = query.Value();
            foreach (var key in source.Keys)
            {
                map.Add(
                    key,
                    source[key]
                );
            }
            return map;
        },
            live: false
        )
        { }
    }
}
