using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Snaps.User;
using Poof.Snaps;
using Poof.Snaps.Flow;
using Yaapii.Atoms;
using Yaapii.Atoms.Text;

namespace Poof.Core.Snaps
{
    public sealed class PublicSnap : ISnap<IInput>
    {
        private readonly IFlow<IInput> flow;

        public PublicSnap(IDataBuilding mem) : this(
            new FwEntity("user")
        )
        { }

        public PublicSnap(IFlow<IInput> flow)
        {
            this.flow = flow;
        }

        public IOutcome<IInput> Convert(IDemand demand)
        {
            var response = this.flow.Response(demand);
            if(!response.Has())
            {
                throw new InvalidOperationException(
                    new Paragraph($"Unable to process public request with parameters",
                        $"entity: {demand.Param("entity")}",
                        $"category: {demand.Param("category")}",
                        $"action: {demand.Param("action")}.",
                        "No valid match was found."
                    ).AsString()
                );
            }

            return response.Value();
        }
    }
}
