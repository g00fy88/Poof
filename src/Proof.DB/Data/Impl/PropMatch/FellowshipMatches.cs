using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Poof.DB.Data.Impl.PropMatch
{
    public sealed class FellowshipMatches : ListEnvelope<DbFellowship>
    {
        public FellowshipMatches(IEnumerable<DbFellowship> fellowships, IPropMatch match) : base(
            new ScalarOf<IEnumerable<DbFellowship>>(()=>
                new FallbackMap<IEnumerable<DbFellowship>>(
                    new MapOf<IEnumerable<DbFellowship>>(
                        new KvpOf<IEnumerable<DbFellowship>>("equals", () =>
                            new FallbackMap<IEnumerable<DbFellowship>>(
                                new MapOf<IEnumerable<DbFellowship>>(
                                    new KvpOf<IEnumerable<DbFellowship>>("type", () =>
                                        fellowships.Where(f => f.Type.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
                                    )
                                ),
                                key => throw new ArgumentException($"Unable to filter transactions for field '{key}', " +
                                        $"because this field cannot be filtered with 'equal'-comparison")
                            )[match.Name()]
                        )
                    ),
                    key => throw new ArgumentException($"Unable to filter transactions, because the filter type '{key}' is unknown.")
                )[match.Type()]
            ),
            false
        )
        { }
    }
}
