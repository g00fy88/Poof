using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Scalar;

namespace Poof.Snaps.Demand
{
    public abstract class DemandEnvelope : IDemand
    {
        private readonly IScalar<IDemand> demand;

        public DemandEnvelope(IDemand demand) : this(
            new ScalarOf<IDemand>(demand)
        )
        { }

        public DemandEnvelope(Func<IDemand> demand) : this(
            new ScalarOf<IDemand>(demand)
        )
        { }

        public DemandEnvelope(IScalar<IDemand> demand)
        {
            this.demand = demand;
        }

        public IInput Body()
        {
            return this.demand.Value().Body();
        }

        public string Param(string name)
        {
            return this.demand.Value().Param(name);
        }

        public string Param(string name, string def)
        {
            return this.demand.Value().Param(name, def);
        }

        public IList<string> Params()
        {
            return this.demand.Value().Params();
        }

        public IDemand Refined(string param, string value)
        {
            return this.demand.Value().Refined(param, value);
        }
    }
}
