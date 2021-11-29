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
    public sealed class TransactionMatches : ListEnvelope<DbTransaction>
    {

        public TransactionMatches(IEnumerable<DbTransaction> transactions, IPropMatch match) : base(
            new ScalarOf<IEnumerable<DbTransaction>>(()=>
                new FallbackMap<IEnumerable<DbTransaction>>(
                    new MapOf<IEnumerable<DbTransaction>>(
                        new KvpOf<IEnumerable<DbTransaction>>("equals", () =>
                            new FallbackMap<IEnumerable<DbTransaction>>(
                                new MapOf<IEnumerable<DbTransaction>>(
                                    new KvpOf<IEnumerable<DbTransaction>>("title", () =>
                                        transactions.Where(t => t.Title.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
                                    ),
                                    new KvpOf<IEnumerable<DbTransaction>>("participant", () =>
                                        transactions.Where(t => t.GiveSide == match.Value<string>() || t.TakeSide == match.Value<string>())
                                    ),
                                    new KvpOf<IEnumerable<DbTransaction>>("giveside", () =>
                                        transactions.Where(t => t.GiveSide == match.Value<string>())
                                    ),
                                    new KvpOf<IEnumerable<DbTransaction>>("givetype", () =>
                                        transactions.Where(t => t.GiveType == match.Value<string>())
                                    ),
                                    new KvpOf<IEnumerable<DbTransaction>>("takeside", () =>
                                        transactions.Where(t => t.TakeSide == match.Value<string>())
                                    ),
                                    new KvpOf<IEnumerable<DbTransaction>>("taketype", () =>
                                        transactions.Where(t => t.TakeType == match.Value<string>())
                                    )
                                ),
                                key => throw new ArgumentException($"Unable to filter transactions for field '{key}', " +
                                        $"because this field cannot be filtered with 'equal'-comparison")
                            )[match.Name()]
                        ),
                        new KvpOf<IEnumerable<DbTransaction>>("sort", () =>
                            new FallbackMap<IEnumerable<DbTransaction>>(
                                new MapOf<IEnumerable<DbTransaction>>(
                                    new KvpOf<IEnumerable<DbTransaction>>("date", () =>
                                        transactions.OrderByDescending(t => t.Date)
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
