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

        public sealed class HasPicture : ScalarEnvelope<bool>
        {
            public HasPicture(IOutcome<IInput> outcome) : base(() =>
                new BoolOf(
                    new JSONOf(outcome.Result()).Value("picture.has")
                ).Value()
            )
            { }
        }

        public sealed class PictureUrl : TextEnvelope
        {
            public PictureUrl(IOutcome<IInput> outcome) : base(() =>
                new JSONOf(outcome.Result()).Value("picture.url"),
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

        public sealed class TakeFactor : ScalarEnvelope<double>
        {
            public TakeFactor(IOutcome<IInput> outcome) : base(() =>
                new DoubleOf(
                    new JSONOf(outcome.Result()).Value("takeFactor")
                ).Value()
            )
            { }
        }

        public sealed class GiveFactor : ScalarEnvelope<double>
        {
            public GiveFactor(IOutcome<IInput> outcome) : base(() =>
                new DoubleOf(
                    new JSONOf(outcome.Result()).Value("giveFactor")
                ).Value()
            )
            { }
        }

        public sealed class Level : ScalarEnvelope<int>
        {
            public Level(IOutcome<IInput> outcome) : base(() =>
                new IntOf(
                    new JSONOf(outcome.Result()).Value("score.level")
                ).Value()
            )
            { }
        }

        public sealed class ScoreNeeded : ScalarEnvelope<double>
        {
            public ScoreNeeded(IOutcome<IInput> outcome) : base(() =>
                new DoubleOf(
                    new JSONOf(outcome.Result()).Value("score.needed")
                ).Value()
            )
            { }
        }

        public sealed class ScoreProgress : ScalarEnvelope<double>
        {
            public ScoreProgress(IOutcome<IInput> outcome) : base(() =>
                new DoubleOf(
                    new JSONOf(outcome.Result()).Value("score.progress")
                ).Value()
            )
            { }
        }
    }
}
