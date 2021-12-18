using Newtonsoft.Json.Linq;
using Poof.Core.Entity.Fellowship;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;

namespace Poof.Core.Snaps.Fellowship
{
    /// <summary>
    /// Checks, if the given name is available as fellowship name
    /// </summary>
    public sealed class CheckNameAvailability : SnapEnvelope<IInput>
    {
        /// <summary>
        /// Checks, if the given name is available as fellowship name
        /// </summary>
        public CheckNameAvailability(IDataBuilding mem) : base(dmd =>
            new JsonRawOutcome(
                new JObject(
                    new JProperty("available",
                        new Fellowships(mem).List(new Name.Match(dmd.Param("name"))).Count == 0
                    )
                )
            )
        )
        { }
    }
}
