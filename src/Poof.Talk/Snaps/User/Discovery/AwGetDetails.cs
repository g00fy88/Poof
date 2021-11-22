using Poof.Snaps;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.User.Discovery
{
    public sealed class AwGetDetails
    {
        public sealed class PseudoName : TextEnvelope
        {
            public PseudoName(IOutcome<IInput> outcome) : base(()=>
                new JSONOf(outcome.Result()).Value("pseudonym.name"),
                false
            )
            { }
        }

        public sealed class PseudoNumber : ScalarEnvelope<int>
        {
            public PseudoNumber(IOutcome<IInput> outcome) : base(() =>
                new IntOf(
                    new JSONOf(outcome.Result()).Value("pseudonym.number")
                ).Value()
            )
            { }
        }

        public sealed class PictureUrl : TextEnvelope
        {
            public PictureUrl(IOutcome<IInput> outcome) : base(() =>
                new JSONOf(outcome.Result()).Value("picture"),
                false
            )
            { }
        }

        public sealed class Points : ScalarEnvelope<double>
        {
            public Points(IOutcome<IInput> outcome) : base(() =>
                new DoubleOf(
                    new JSONOf(outcome.Result()).Value("points")
                ).Value()
            )
            { }
        }

        public sealed class Score : ScalarEnvelope<double>
        {
            public Score(IOutcome<IInput> outcome) : base(() =>
                new DoubleOf(
                    new JSONOf(outcome.Result()).Value("score")
                ).Value()
            )
            { }
        }
    }
}
