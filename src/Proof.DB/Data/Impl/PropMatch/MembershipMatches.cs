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
    public sealed class MembershipMatches : ListEnvelope<DbMembership>
    {
        public MembershipMatches(IEnumerable<DbMembership> memberships, IPropMatch match) : base(
            new ScalarOf<IEnumerable<DbMembership>>(()=>
                new FallbackMap<IEnumerable<DbMembership>>(
                    new MapOf<IEnumerable<DbMembership>>(
                        new KvpOf<IEnumerable<DbMembership>>("equals", () =>
                            new FallbackMap<IEnumerable<DbMembership>>(
                                new MapOf<IEnumerable<DbMembership>>(
                                    new KvpOf<IEnumerable<DbMembership>>("owner", () =>
                                        memberships.Where(m => m.Owner.Id.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
                                    ),
                                    new KvpOf<IEnumerable<DbMembership>>("team", () =>
                                        memberships.Where(m => m.Team.Id.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
                                    ),
                                    new KvpOf<IEnumerable<DbMembership>>("role", () =>
                                        memberships.Where(m => m.Role.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
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
