using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Map;

namespace Poof.Web.Server.Request
{
    /// <summary>
    /// Validates the given request.
    /// Validation is made only, if the request has a body.
    /// In this case the verb and the content type of the request is validated
    /// </summary>
    public sealed class StrictBody : IBytes
    {
        private readonly string[] CHARSETS = new string[] { "utf-7", "utf-8", "utf-16", "utf-32" };
        private readonly HttpRequest request;
        private readonly Action<Exception> handling;
        private readonly IDictionary<string, Action> validations;
        private readonly IBytes origin;

        /// <summary>
        /// Validates the given request.
        /// Validation is made only, if the request has a body.
        /// In this case the verb and the content type of the request is validated
        /// </summary>
        public StrictBody(HttpRequest request, IBytes origin) : this(
            request,
            ex => throw ex,
            origin
        )
        { }

        /// <summary>
        /// Validates the given request.
        /// Validation is made only, if the request has a body.
        /// In this case the verb and the content type of the request is validated
        /// </summary>
        public StrictBody(HttpRequest request, Action<Exception> handling, IBytes origin)
        {
            this.request = request;
            this.handling = handling;
            this.origin = origin;
            this.validations =
                new MapOf<Action>(
                    new KvpOf<Action>(
                        "application/json",
                        () => ValidateJson()
                    ),
                    new KvpOf<Action>(
                        "application/zip",
                        () => { }
                    ),
                    new KvpOf<Action>(
                        "multipart/form-data",
                        () => { }
                    ),
                    new KvpOf<Action>(
                        "text/plain",
                        () => { }
                    )
                );
        }

        public byte[] AsBytes()
        {
            var headers =
                new Mapped<string, string>(
                    h => h.ToLower(),
                    this.request.Headers.Keys
                );
            if (this.request.ContentLength != null && this.request.ContentLength > 0)
            {
                ValidateBasics(headers);
                ValidateRequest();
            }

            return this.origin.AsBytes();
        }

        private void ValidateBasics(ICollection<string> headers)
        {
            if (this.request.Method == "GET" || this.request.Method == "DELETE")
            {
                this.handling.Invoke(new InvalidOperationException($"Request of type '{this.request.Method}' has no body."));
            }
            else if (!headers.Contains("content-type"))
            {
                this.handling.Invoke(new ArgumentException($"Could not interpret body, as no Content-Type was given."));
            }
        }

        private void ValidateRequest()
        {
            var contentType = this.request.ContentType.ToLower();
            var exists = false;
            foreach (var type in this.validations.Keys)
            {
                if (contentType.StartsWith(type.ToLower()))
                {
                    this.validations[type].Invoke();
                    exists = true;
                }
            }
            if (!exists)
            {
                this.handling(
                    new ArgumentException(
                        $"Could not interpret payload as Content-Type: \"{contentType}\" is not supported. " +
                        $"Supported Content-Types are: \"application/json\", \"application/zip\" and \"multipart/form-data.\""
                    )
                );
            }
        }

        private void ValidateJson()
        {
            var encoding = ContentEncoding(this.request.ContentType);
            if (encoding != Encoding.UTF8)
            {
                this.handling(
                    new ArgumentException(
                        $"Unable to read json body with given encoding '{encoding.ToString()}'. " +
                        $"Only 'utf-8' encoding is allowed for json body."
                    )
                );
            }
        }

        private Encoding ContentEncoding(string contentType)
        {
            Encoding enc = Encoding.Default;
            bool valid = false;
            foreach (var charset in CHARSETS)
            {
                if (contentType.EndsWith($"charset={charset}"))
                {
                    enc = Encoding.GetEncoding(charset);
                    valid = true;
                    break;
                }
            }
            if (!valid)
            {
                throw new ArgumentException($"Cannot interpret body using '{contentType}' - charset is not supported.");
            }
            return enc;
        }
    }
}
