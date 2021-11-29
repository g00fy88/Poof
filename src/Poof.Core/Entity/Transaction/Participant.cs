using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Entity.Transaction
{
    public sealed class Participant
    {
        public sealed class Match : PropMatchEnvelope
        {
            public Match(string id) : base(
                "participant",
                "equals",
                id
            )
            { }
        }
    }
}
