using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Poof.DB.Models
{
    public sealed class DbValue<TEntity, TValue> : IFunc<string, TValue> where TEntity : class
    {
        private readonly IDictionary<Type, IDictionary<string, TValue>> map;
        private readonly TEntity entity;

        public DbValue(TEntity entity)
        {
            this.map =
                new MapOf<Type, IDictionary<string, TValue>>(
                    new KvpOf<Type, IDictionary<string, TValue>>(
                        typeof(ApplicationUser),
                        () => new MapOf<TValue>(
                            new KvpOf<TValue>("mail", () => Cast<TValue>("mail", (entity as ApplicationUser).Email)),
                            new KvpOf<TValue>("pseudonym", () => Cast<TValue>("pseudonym", (entity as ApplicationUser).Pseudonym)),
                            new KvpOf<TValue>("pseudonumber", () => Cast<TValue>("pseudonumber", (entity as ApplicationUser).PseudoNumber)),
                            new KvpOf<TValue>("points", () => Cast<TValue>("points", (entity as ApplicationUser).Points)),
                            new KvpOf<TValue>("goodscore", () => Cast<TValue>("goodscore", (entity as ApplicationUser).GoodScore)),
                            new KvpOf<TValue>("balancescore", () => Cast<TValue>("balancescore", (entity as ApplicationUser).BalanceScore))
                        )
                    ),
                    new KvpOf<Type, IDictionary<string, TValue>>(
                        typeof(DbFellowship),
                        () => new MapOf<TValue>(
                            new KvpOf<TValue>("name", () => Cast<TValue>("name", (entity as DbFellowship).Name))
                        )
                    ),
                    new KvpOf<Type, IDictionary<string, TValue>>(
                        typeof(DbTransaction),
                        () => new MapOf<TValue>(
                            new KvpOf<TValue>("amount", () => Cast<TValue>("amount", (entity as DbTransaction).Amount)),
                            new KvpOf<TValue>("date", () => Cast<TValue>("date", (entity as DbTransaction).Date)),
                            new KvpOf<TValue>("giveside", () => Cast<TValue>("giveside", (entity as DbTransaction).GiveSide)),
                            new KvpOf<TValue>("givetype", () => Cast<TValue>("givetype", (entity as DbTransaction).GiveType)),
                            new KvpOf<TValue>("takeside", () => Cast<TValue>("takeside", (entity as DbTransaction).TakeSide)),
                            new KvpOf<TValue>("taketype", () => Cast<TValue>("taketype", (entity as DbTransaction).TakeType))
                        )
                    ),
                    new KvpOf<Type, IDictionary<string, TValue>>(
                        typeof(DbMembership),
                        () => new MapOf<TValue>(
                            new KvpOf<TValue>("owner", () => Cast<TValue>("owner", (entity as DbMembership).Owner.Id)),
                            new KvpOf<TValue>("team", () => Cast<TValue>("team", (entity as DbMembership).Team.Id)),
                            new KvpOf<TValue>("share", () => Cast<TValue>("share", (entity as DbMembership).Share))
                        )
                    )
                );
            this.entity = entity;
        }

        public TValue Invoke(string name)
        {
            return 
                new FallbackMap<TValue>(
                    new FallbackMap<Type, IDictionary<string, TValue>>(
                        this.map,
                        key => throw new InvalidOperationException($"Unable to retrive value, because the given entity has an invalid type '{key}'")
                    )[this.entity.GetType()],
                    key => throw new InvalidOperationException($"Unable to retrieve property '{name}', because it does not exist.")
                )[name];

        }

        private T Cast<T>(string name, object value)
        {
            new FailNull(value,
                new InvalidOperationException($"Unable to retrieve property '{name}', because it is not set.")
            ).Go();

            var result = (T)Convert.ChangeType(value, typeof(T));

            new FailNull(result,
                new InvalidOperationException($"Unable to cast property '{name}', because the wrong type was specified.")
            ).Go();

            return result;
        }
    }
}
