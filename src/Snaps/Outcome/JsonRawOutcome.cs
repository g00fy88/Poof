using Newtonsoft.Json.Linq;
using Yaapii.Atoms;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.JSON;

namespace Poof.Snaps.Outcome
{
    /// <summary>
    /// Outcome from json.
    /// </summary>
    public sealed class JsonRawOutcome : OutcomeEnvelope<IInput>
    {
        /// <summary>
        /// outcome from json
        /// </summary>
        public JsonRawOutcome(JToken jobj) : base(
            new ScalarOf<IOutcome<IInput>>(() => 
                new OutcomeOf<IInput>(
                    new InputOf(jobj.ToString()),
                    "json"
                )
            )
        )
        { }

        /// <summary>
        /// outcome from json
        /// </summary>
        public JsonRawOutcome(IJSON json) : base(
            new ScalarOf<IOutcome<IInput>>(() =>
                new OutcomeOf<IInput>(
                    new InputOf(json.Token().ToString()),
                    "json"
                )
            )
        )
        { }
    }
}
