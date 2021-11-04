using Yaapii.Atoms;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Text;

namespace Poof.Snaps.Outcome
{
    /// <summary>
    /// Text raw input outcome.
    /// </summary>
    public sealed class TextRawOutcome : OutcomeEnvelope<IInput>
    {
        /// <summary>
        /// Text raw input outcome.
        /// </summary>
        public TextRawOutcome(string text) : this(new TextOf(text))
        { }

        /// <summary>
        /// Text raw input outcome.
        /// </summary>
        public TextRawOutcome(IText text) : base(
            new OutcomeOf<IInput>(
                new InputOf(text), "text"
            )
        )
        { }
    }
}
