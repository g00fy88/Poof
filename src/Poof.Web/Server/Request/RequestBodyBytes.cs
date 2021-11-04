using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using Yaapii.Atoms;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Poof.Web.Server.Request
{
    /// <summary>
    /// Reads the request body based on the content type.
    /// </summary>
    public sealed class RequestBodyBytes : IBytes
    {
        private readonly IScalar<IInput> body;
        /// <summary>
        /// Reads the request body based on the content type.
        /// </summary>
        public RequestBodyBytes(ControllerBase controller) : this(
            controller,
            new MapOf<string, IInput>(
                    new KeyValuePair<string, IInput>(
                        "application/json",
                        new RawBody(controller)
                    ),
                    new KeyValuePair<string, IInput>(
                        "application/zip",
                        new RawBody(controller)
                    ),
                    new KeyValuePair<string, IInput>(
                        "multipart/form-data",
                        new FileBody(controller)
                    ),
                    new KeyValuePair<string, IInput>(
                        "text/plain",
                        new TextBody(controller)
                    )
                )
        )
        { }

        private RequestBodyBytes(ControllerBase controller, IDictionary<string, IInput> contentTypes)
        {
            this.body = new ScalarOf<IInput>(() => BodyFromController(controller, contentTypes));
        }

        public byte[] AsBytes()
        {
            return new BytesOf(this.body.Value()).AsBytes();
        }

        private IInput BodyFromController(ControllerBase controller, IDictionary<string, IInput> contentTypes)
        {
            IInput content = new DeadInput();
            var headers =
                new Mapped<string, string>(
                    h => h.ToLower(),
                    controller.Request.Headers.Keys
                );
            if (headers.Contains("content-type"))
            {
                content = Content(controller, contentTypes);
            }
            return content;
        }

        private IInput Content(ControllerBase controller, IDictionary<string, IInput> contentTypes)
        {
            IInput content = new DeadInput();
            foreach (var type in contentTypes)
            {
                if (controller.Request.ContentType.ToLower().StartsWith(type.Key.ToLower()))
                {
                    content = contentTypes[type.Key];
                }
            }
            return content;
        }
    }
}
