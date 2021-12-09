using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms;

namespace Poof.Core.Snaps.Fellowship
{
    public sealed class AddsFellowship : SnapEnvelope<IInput>
    {
        public AddsFellowship(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
