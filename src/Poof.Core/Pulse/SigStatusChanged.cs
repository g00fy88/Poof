using Poof.Core.Model;
using Pulse;
using Pulse.Signal;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Pulse
{
    public sealed class SigStatusChanged : SignalEnvelope
    {
        public SigStatusChanged(string user, string statusName) : base(()=>
            new SignalOf(
                new SigHead("user", "status", "changed"),
                new SigProps(
                    "user", user,
                    "name", statusName
                )
            )
        )
        { }

        public sealed class Is : ScalarEnvelope<bool>
        {
            public Is(ISignal signal) : base(()=>
                new SigHead.Is(signal, "user", "status", "changed").Value()
            )
            { }
        }

        public sealed class User : TextEnvelope
        {
            public User(ISignal signal) : base(()=>
                signal.Props()["user"],
                false
            )
            { }
        }

        public sealed class Name : TextEnvelope
        {
            public Name(ISignal signal) : base(() =>
                signal.Props()["name"],
                false
            )
            { }
        }
    }
}
