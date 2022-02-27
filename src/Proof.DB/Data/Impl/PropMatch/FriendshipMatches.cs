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
    public sealed class FriendshipMatches : ListEnvelope<DbFriendship>
    {
        public FriendshipMatches(IEnumerable<DbFriendship> memberships, IPropMatch match) : base(
            new ScalarOf<IEnumerable<DbFriendship>>(()=>
                new FallbackMap<IEnumerable<DbFriendship>>(
                    new MapOf<IEnumerable<DbFriendship>>(
                        new KvpOf<IEnumerable<DbFriendship>>("equals", () =>
                            new FallbackMap<IEnumerable<DbFriendship>>(
                                new MapOf<IEnumerable<DbFriendship>>(
                                    new KvpOf<IEnumerable<DbFriendship>>("requester", () =>
                                        memberships.Where(m => m.Requester.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
                                    ),
                                    new KvpOf<IEnumerable<DbFriendship>>("friend", () =>
                                        memberships.Where(m => m.Friend.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
                                    ),
                                    new KvpOf<IEnumerable<DbFriendship>>("status", () =>
                                        memberships.Where(m => m.Status.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
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
