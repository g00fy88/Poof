using Microsoft.AspNetCore.Mvc;
using Yaapii.Atoms;

namespace Poof.Web.Server.Response
{
    /// <summary>
    /// A response of type zip.
    /// </summary>
    public sealed class RsZip : IScalar<FileStreamResult>
    {
        private readonly IInput result;

        /// <summary>
        /// A response of type zip.
        /// </summary>
        public RsZip(IInput result)
        {
            this.result = result;
        }

        public FileStreamResult Value()
        {
            return new FileStreamResult(this.result.Stream(), "application/zip");
        }
    }
}
