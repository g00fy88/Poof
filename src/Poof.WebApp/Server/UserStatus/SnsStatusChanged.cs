using Poof.Core.Pulse;
using Pulse.Sensor;
using Pulse.Signal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Poof.WebApp.Server.UserStatus
{
    /// <summary>
    /// Sensor that reacts to status changed signal.
    /// Adds the user status to the given status
    /// </summary>
    public sealed class SnsStatusChanged : SensorEnvelope
    {
        /// <summary>
        /// Sensor that reacts to status changed signal.
        /// Adds the user status to the given status
        /// </summary>
        public SnsStatusChanged(IIdentityStatus status) : base(()=>
            new ConditionalSensor(
                new SnsAct(sig => 
                    status.Add(
                        new SigStatusChanged.User(sig).AsString(),
                        new SigStatusChanged.Name(sig).AsString()
                    )
                ),
                sig => new SigStatusChanged.Is(sig).Value()
            )
        )
        { }
    }
}
