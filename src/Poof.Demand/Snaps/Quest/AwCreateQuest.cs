using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Demand.Snaps.Quest
{
    public sealed class AwCreateQuest
    {
        public sealed class Id : TextEnvelope
        {
            public Id(IOutcome<IInput> outcome) : base(()=>
                new JSONOf(outcome.Result()).Value("id"),
                false
            )
            { }
        }
    }
}
