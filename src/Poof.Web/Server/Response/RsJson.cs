using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Web.Server.Response
{
    /// <summary>
    /// A ASPNET Core Content result of type application/json, built from a JSON text.
    /// </summary>
    public sealed class RsJson : IScalar<IActionResult>
    {
        private readonly IScalar<IText> json;
        private readonly Encoding encoding;

        /// <summary>
        /// A ASPNET Core Content result of type application/json, built from a JSON text.
        /// </summary>
        public RsJson(IEnumerable<string> jsonObjects, Encoding encoding) : this(new Live<IText>(() =>
        {
            return
                new TextOf(
                    $"[{new Joined(",", jsonObjects).AsString()}]",
                    encoding
                );
        }),
            encoding
        )
        { }

        /// <summary>
        /// A ASPNET Core Content result of type application/json, built from a JSON text.
        /// </summary>
        public RsJson(JArray arr, Encoding encoding) : this(
            new ScalarOf<IText>(
                () => new TextOf(arr.ToString(), encoding)
            ),
            encoding
        )
        { }

        /// <summary>
        /// A ASPNET Core Content result of type application/json, built from a JSON text.
        /// </summary>
        public RsJson(JToken token, Encoding encoding) : this(
            new ScalarOf<IText>(
                () => new TextOf(token.ToString(), encoding)
            ),
            encoding
        )
        { }

        /// <summary>
        /// A ASPNET Core Content result of type application/json, built from a JSON text.
        /// </summary>
        public RsJson(JObject obj, Encoding encoding) : this(
            new ScalarOf<IText>(
                () => new TextOf(obj.ToString(), encoding)
            ),
            encoding
        )
        { }

        /// <summary>
        /// A ASPNET Core Content result of type application/json, built from a JSON text.
        /// </summary>
        public RsJson(string json) : this(new TextOf(json, Encoding.Unicode), Encoding.Unicode)
        { }

        /// <summary>
        /// A ASPNET Core Content result of type application/json, built from a JSON text.
        /// </summary>
        public RsJson(IInput json, Encoding encoding) : this(new TextOf(json, encoding), encoding)
        {
        }

        /// <summary>
        /// A ASPNET Core Content result of type application/json, built from a JSON text.
        /// </summary>
        public RsJson(IText json) : this(new Live<IText>(json), Encoding.UTF8)
        { }

        /// <summary>
        /// A ASPNET Core Content result of type application/json, built from a JSON text.
        /// </summary>
        public RsJson(IText json, Encoding encoding) : this(new Live<IText>(json), encoding)
        { }

        /// <summary>
        /// A ASPNET Core Content result of type application/json, built from a JSON text.
        /// </summary>
        private RsJson(IScalar<IText> json, Encoding encoding)
        {
            this.json = json;
            this.encoding = encoding;
        }

        /// <summary>
        /// ASPNET Core Result.
        /// </summary>
        /// <returns></returns>
        public IActionResult Value()
        {
            var res = new ContentResult();
            res.StatusCode = 200;
            res.Content = this.json.Value().AsString();
            res.ContentType = $"application/json; charset={this.encoding.WebName}";
            return res;
        }
    }
}
