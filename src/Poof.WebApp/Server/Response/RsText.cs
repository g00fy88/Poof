using Microsoft.AspNetCore.Mvc;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Web.Server.Response
{
    /// <summary>
    /// A text as a <see cref="IActionResult"/>
    /// </summary>
    public sealed class RsText : ScalarEnvelope<IActionResult>
    {
        /// <summary>
        /// A text as a <see cref="IActionResult"/>
        /// </summary>
        public RsText(IInput content, Encoding encoding) : this(
            new TextOf(content, encoding),
            encoding
        )
        { }

        /// <summary>
        /// A text as a <see cref="IActionResult"/>
        /// </summary>
        public RsText(IText content) : this(
            new Live<ContentResult>(() =>
                new ContentResult()
                {
                    Content = content.AsString(),
                    StatusCode = 200
                }
            ),
            Encoding.UTF8
        )
        { }

        /// <summary>
        /// A text as a <see cref="IActionResult"/>
        /// </summary>
        public RsText(IText content, Encoding encoding) : this(
            new Live<ContentResult>(() =>
                 new ContentResult()
                 {
                     Content = content.AsString(),
                     StatusCode = 200
                 }
            ),
            encoding
        )
        { }

        /// <summary>
        /// A text as a <see cref="IActionResult"/>
        /// </summary>
        public RsText(string content) : this(
            new ScalarOf<ContentResult>(()=>
                 new ContentResult()
                 {
                     Content = content,
                     StatusCode = 200
                 }
            ),
            Encoding.UTF8
        )
        { }

        /// <summary>
        /// A text as a <see cref="IActionResult"/>
        /// </summary>
        public RsText(IScalar<ContentResult> origin, Encoding encoding) : base(() =>
        {
            var result = origin.Value();
            result.ContentType = $"text/plain; charset={encoding.WebName}";
            return result;
        })
        { }
    }
}
