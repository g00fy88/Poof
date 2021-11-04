using Yaapii.Atoms;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.Xml;

namespace Poof.Snaps.Outcome
{
    /// <summary>
    /// Outcome from xml.
    /// </summary>
    public sealed class XmlRawOutcome : OutcomeEnvelope<IInput>
    {
        /// <summary>
        /// Outcome from xml.
        /// </summary>
        public XmlRawOutcome(IXML xml): base(
            new ScalarOf<IOutcome<IInput>>(() => 
                new OutcomeOf<IInput>(
                    new InputOf(xml.AsNode().ToString()), 
                    "xml"
                )
            )
        )
        { }
    }
}
