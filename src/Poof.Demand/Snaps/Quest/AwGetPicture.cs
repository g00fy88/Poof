using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Demand.Snaps.Quest
{
    public sealed class AwGetPicture
    {
        public sealed class Url : TextEnvelope
        {
            public Url(IOutcome<IInput> outcome) : base(()=>
                new JSONOf(outcome.Result()).Value("url"),
                false
            )
            { }
        }
    }
}
