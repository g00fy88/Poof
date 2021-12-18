using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.Fellowship
{
    public sealed class AwCheckNameAvailability
    {
        public sealed class Available : ScalarEnvelope<bool>
        {
            public Available(IOutcome<IInput> outcome) : base(()=>
                new BoolOf(
                    new JSONOf(
                        outcome.Result()
                    ).Value("available")
                ).Value()
            )
            { }
        }
    }
}
