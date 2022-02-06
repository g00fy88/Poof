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
    public sealed class QuestMatches : ListEnvelope<DbQuest>
    {
        public QuestMatches(IEnumerable<DbQuest> quests, IPropMatch match) : base(
            new ScalarOf<IEnumerable<DbQuest>>(()=>
                new FallbackMap<IEnumerable<DbQuest>>(
                    new MapOf<IEnumerable<DbQuest>>(
                        new KvpOf<IEnumerable<DbQuest>>("equals", () =>
                            new FallbackMap<IEnumerable<DbQuest>>(
                                new MapOf<IEnumerable<DbQuest>>(
                                    new KvpOf<IEnumerable<DbQuest>>("title", () =>
                                        quests.Where(t => t.Title.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
                                    ),
                                    new KvpOf<IEnumerable<DbQuest>>("scope", () =>
                                        quests.Where(t => t.Scope.Equals(match.Value<string>(), StringComparison.OrdinalIgnoreCase))
                                    ),
                                    new KvpOf<IEnumerable<DbQuest>>("issuer", () =>
                                        quests.Where(t => t.Issuer != null && t.Issuer == match.Value<string>())
                                    ),
                                    new KvpOf<IEnumerable<DbQuest>>("applicant", () =>
                                        quests.Where(t => t.Applicant != null && t.Applicant == match.Value<string>())
                                    ),
                                    new KvpOf<IEnumerable<DbQuest>>("status", () =>
                                        quests.Where(t => t.Status == match.Value<string>())
                                    )
                                ),
                                key => throw new ArgumentException($"Unable to filter transactions for field '{key}', " +
                                        $"because this field cannot be filtered with 'equal'-comparison")
                            )[match.Name()]
                        ),
                        new KvpOf<IEnumerable<DbQuest>>("sort", () =>
                            new FallbackMap<IEnumerable<DbQuest>>(
                                new MapOf<IEnumerable<DbQuest>>(
                                    new KvpOf<IEnumerable<DbQuest>>("end-date", () =>
                                        quests.OrderByDescending(t => t.EndDate)
                                    )
                                ),
                                key => throw new ArgumentException($"Unable to filter transactions for field '{key}', " +
                                        $"because this field cannot be filtered with 'sort'-comparison")
                            )[match.Name()]
                        ),
                        new KvpOf<IEnumerable<DbQuest>>("not-equal", () =>
                            new FallbackMap<IEnumerable<DbQuest>>(
                                new MapOf<IEnumerable<DbQuest>>(
                                    new KvpOf<IEnumerable<DbQuest>>("issuer", () =>
                                        quests.Where(q => q.Issuer != match.Value<string>())
                                    )
                                ),
                                key => throw new ArgumentException($"Unable to filter transactions for field '{key}', " +
                                        $"because this field cannot be filtered with 'not-equal'-comparison")
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
