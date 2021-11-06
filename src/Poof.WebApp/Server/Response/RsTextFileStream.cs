using Microsoft.AspNetCore.Mvc;
using Yaapii.Atoms;

namespace Poof.Web.Server.Response
{
    /// <summary>
    /// A file stream result wrapping a basic input text.
    /// </summary>
    public sealed class RsTextFileStream : IScalar<FileStreamResult>
    {
        private readonly IInput content;

        /// <summary>
        /// A file stream result wrapping a basic input text.
        /// </summary>
        public RsTextFileStream(IInput content)
        {
            this.content = content;
        }

        public FileStreamResult Value()
        {
            return new FileStreamResult(content.Stream(), "text/plain; charset=utf-8");
        }
    }
}
