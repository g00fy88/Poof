using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Web.Server.Request
{
    /// <summary>
    /// The payload of an Asp.Net.core Request form plaintext
    /// </summary>
    public sealed class TextBody : IInput
    {
        private readonly IScalar<IInput> body;

        /// <summary>
        /// The payload of an Asp.Net.core Request form plaintext
        /// </summary>
        public TextBody(ControllerBase controller) :
            this(
                controller,
                new ManyOf("utf-7", "utf-8", "utf-16", "utf-32")
            )
        { }

        /// <summary>
        /// The payload of an Asp.Net.core Request form plaintext
        /// </summary>
        public TextBody(ControllerBase controller, IEnumerable<string> charsets)
        {
            this.body =
                new ScalarOf<IInput>(() =>
                    new InputOf(
                        new TextOf(
                            new StreamReader(
                                controller.Request.Body,
                                Encoding.GetEncoding(Charset(controller, charsets))
                            )
                        ).AsString()
                    )
                );
        }

        public Stream Stream()
        {
            return this.body.Value().Stream();
        }

        private string Charset(ControllerBase controller, IEnumerable<string> charsets)
        {
            var contentType = controller.Request.ContentType.ToLower();

            var filtered = new Filtered<string>(
                (str) => contentType.EndsWith($"charset={str}"),
                charsets);

            new FailEmpty<string>(
                filtered,
                new ArgumentException(
                    $"Cannot work with content '{contentType}' - charset is not supported."
                )
            ).Go();

            return new FirstOf<string>(filtered).Value();
        }
    }
}
