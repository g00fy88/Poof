using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using Yaapii.Atoms;

namespace Poof.Web.Server.Request
{
    /// <summary>
    /// The payload of an Asp.Net.core Request form file
    /// </summary>
    public sealed class FileBody : IInput
    {
        private readonly ControllerBase controller;

        /// <summary>
        /// The payload of an Asp.Net.core Request form file
        /// </summary>
        public FileBody(ControllerBase controller)
        {
            this.controller = controller;
        }

        public Stream Stream()
        {
            Stream content = new MemoryStream();

            if (this.controller.Request.Form.Files.Count == 0)
            {
                throw new ArgumentException("Could not read a file as none was supplied.");
            }
            this.controller.Request.Form.Files[0].CopyTo(content);
            content.Seek(0, SeekOrigin.Begin);

            return content;
        }
    }
}
