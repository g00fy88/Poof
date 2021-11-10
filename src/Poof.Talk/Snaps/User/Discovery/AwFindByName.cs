using Poof.Snaps;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Text;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.User.Discovery
{
    public sealed class AwFindByName
    {
        public sealed class List : ListEnvelope<IJSON>
        {
            public List(IOutcome<IInput> outcome) : base(()=>
                new JSONOf(outcome.Result()).Nodes("[*]"),
                false
            )
            { }
        }
    }
}
