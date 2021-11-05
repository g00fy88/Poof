using Poof.DB.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Poof.DB.Models
{
    public sealed class DbUpdate<TEntity, TValue> : IAction<TValue>
    {
        private readonly IDictionary<Type, IDictionary<string, Action<TValue>>> map;
        private readonly TEntity entity;
        private readonly string name;

        public DbUpdate(TEntity entity, string name, ApplicationDbContext context) : this(
            entity,
            name,
            id => context.Users.Find(id),
            id => context.Fellowships.Find(id)
        )
        { }

        public DbUpdate(TEntity entity, string name, Func<string, ApplicationUser> user, Func<string, DbFellowship> fellowship)
        {
            this.map =
                new MapOf<Type, IDictionary<string, Action<TValue>>>(
                    new KvpOf<Type, IDictionary<string, Action<TValue>>>(
                        typeof(ApplicationUser),
                        () => new MapOf<Action<TValue>>(
                            new KvpOf<Action<TValue>>("mail", val => (entity as ApplicationUser).Email = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("pseudonym", val => (entity as ApplicationUser).Pseudonym = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("points", val => (entity as ApplicationUser).Points = Cast<double>(name, val)),
                            new KvpOf<Action<TValue>>("goodscore", val => (entity as ApplicationUser).GoodScore = Cast<double>(name, val)),
                            new KvpOf<Action<TValue>>("balancescore", val => (entity as ApplicationUser).BalanceScore = Cast<double>(name, val))
                        )
                    ),
                    new KvpOf<Type, IDictionary<string, Action<TValue>>>(
                        typeof(DbFellowship),
                        () => new MapOf<Action<TValue>>(
                            new KvpOf<Action<TValue>>("name", val => (entity as DbFellowship).Name = Cast<string>(name, val))
                        )
                    ),
                    new KvpOf<Type, IDictionary<string, Action<TValue>>>(
                        typeof(DbTransaction),
                        () => new MapOf<Action<TValue>>(
                            new KvpOf<Action<TValue>>("amount", val => (entity as DbTransaction).Amount = Cast<double>(name, val)),
                            new KvpOf<Action<TValue>>("date", val => (entity as DbTransaction).Date = Cast<DateTime>(name, val)),
                            new KvpOf<Action<TValue>>("giveside", val => (entity as DbTransaction).GiveSide = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("givetype", val => (entity as DbTransaction).GiveType = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("takeside", val => (entity as DbTransaction).TakeSide = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("taketype", val => (entity as DbTransaction).TakeType = Cast<string>(name, val))
                        )
                    ),
                    new KvpOf<Type, IDictionary<string, Action<TValue>>>(
                        typeof(DbMembership),
                        () => new MapOf<Action<TValue>>(
                            new KvpOf<Action<TValue>>("owner", val => (entity as DbMembership).Owner = user(Cast<string>(name, val))),
                            new KvpOf<Action<TValue>>("team", val => (entity as DbMembership).Team = fellowship(Cast<string>(name, val))),
                            new KvpOf<Action<TValue>>("share", val => (entity as DbMembership).Share = Cast<double>(name, val))
                        )
                    )
                );
            this.entity = entity;
            this.name = name;
        }

        public void Invoke(TValue input)
        {
            var type =
                new FirstOf<Type>(
                    t => t.IsAssignableFrom(this.entity.GetType()),
                    this.map.Keys,
                    new InvalidOperationException("Unable to update value, because the given entity has an invalid type")
                ).Value();

            new FallbackMap<Action<TValue>>(
                this.map[type],
                key => throw new InvalidOperationException($"Unable to update property '{name}', because it does not exist.")
            )[name].Invoke(input);
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
