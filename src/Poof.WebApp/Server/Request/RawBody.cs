using Microsoft.AspNetCore.Mvc;
using System.IO;
using Yaapii.Atoms;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;

namespace Poof.Web.Server.Request
{
    /// <summary>
    /// The payload of an Asp.Net.core Request from a file stream
    /// </summary>
    public sealed class RawBody : IInput
    {
        private readonly IScalar<IInput> content;

        /// <summary>
        /// The payload of an Asp.Net.core Request from a file stream
        /// </summary>
        public RawBody(ControllerBase controller)
        {
            this.content =
                new ScalarOf<IInput>(() =>
                {
                    return
                        new InputOf(
                            new BytesOf(
                                new InputOf(controller.Request.Body)
                            ).AsBytes()
                        );
                });
        }

        public Stream Stream()
        {
            return this.content.Value().Stream();
        }
    }
}
