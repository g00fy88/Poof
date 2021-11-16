using Microsoft.AspNetCore.Mvc;
using Poof.Snaps;
using System;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Poof.Web.Server.Response
{
    /// <summary>
    /// Can be any ActionResult depends on the outcome params
    /// </summary>
    public sealed class RsApi : IScalar<IActionResult>
    {
        private readonly IScalar<IActionResult> result;

        public RsApi(ISnap<IInput> snap, IDemand demand) : this(
            new ScalarOf<IOutcome<IInput>>(() => snap.Convert(demand))
        )
        { }

        public RsApi(IOutcome<IInput> outcome) : this(new ScalarOf<IOutcome<IInput>>(outcome))
        { }

        /// <summary>
        /// Can be any ActionResult depends on the outcome params
        /// </summary>
        public RsApi(IScalar<IOutcome<IInput>> outcome)
        {
            this.result = new ScalarOf<IActionResult>(() =>
            {
                var content =
                    new Ternary<IOutcome<IInput>, IInput>(
                        outcome.Value(),
                        o => o.IsEmpty(),
                        o => new DeadInput(),
                        o => o.Result()
                    );
                var map =
                    new MapOf<IActionResult>(
                        new KvpOf<IActionResult>("empty", () => new ContentResult() { StatusCode = 200 }),
                        new KvpOf<IActionResult>("text", () => new RsText(content.Value(), Encoding.UTF8).Value()),
                        new KvpOf<IActionResult>("filestream", () => new RsTextFileStream(content.Value()).Value()),
                        new KvpOf<IActionResult>("json", () => new RsJson(content.Value(), Encoding.UTF8).Value()),
                        new KvpOf<IActionResult>("zip", () => new RsZip(content.Value()).Value())
                    );
                var format = outcome.Value().Param("format");

                if (map.ContainsKey(format))
                {
                    return map[format];
                }
                throw new ApplicationException($"Snaps outcome had the unexpected format {format}. Valid formats are: {string.Join(", ", map.Keys)}");
            });
        }

        public IActionResult Value()
        {
            return result.Value();
        }
    }
}
