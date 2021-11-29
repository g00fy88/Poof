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
    public sealed class UserMatches : ListEnvelope<ApplicationUser>
    {

        public UserMatches(IEnumerable<ApplicationUser> users, IPropMatch match) : base(
            new ScalarOf<IEnumerable<ApplicationUser>>(()=>
                new FallbackMap<IEnumerable<ApplicationUser>>(
                    new MapOf<IEnumerable<ApplicationUser>>(
                        new KvpOf<IEnumerable<ApplicationUser>>("equals", () =>
                            new FallbackMap<IEnumerable<ApplicationUser>>(
                                new MapOf<IEnumerable<ApplicationUser>>(
                                    new KvpOf<IEnumerable<ApplicationUser>>("mail", () =>
                                        users.Where(u => u.Email == match.Value<string>())
                                    ),
                                    new KvpOf<IEnumerable<ApplicationUser>>("pseudonym", () =>
                                        users.Where(u => u.Pseudonym == match.Value<string>())
                                    ),
                                    new KvpOf<IEnumerable<ApplicationUser>>("pseudonumber", () =>
                                        users.Where(u => u.PseudoNumber == match.Value<int>())
                                    ),
                                    new KvpOf<IEnumerable<ApplicationUser>>("friends", () =>
                                        users.Where(u => u.Friends == match.Value<string>())
                                    )
                                ),
                                key => throw new ArgumentException($"Unable to filter users for field '{key}', " +
                                        $"because this field cannot be filtered with 'equal'-comparison")
                            )[match.Name()]
                        )
                    ),
                    key => throw new ArgumentException($"Unable to filter users, because the filter type '{key}' is unknown.")
                )[match.Type()]
            ),
            false
        )
        { }
    }
}
