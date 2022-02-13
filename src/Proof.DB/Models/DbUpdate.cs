using Poof.DB.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Error;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Poof.DB.Models
{
    public sealed class DbUpdate<TEntity, TValue> : IFunc<TValue, TEntity>
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

        public DbUpdate(TEntity entity, string name, Func<string, ApplicationUser> tuser, Func<string, DbFellowship> tfellowship)
        {
            this.map =
                new MapOf<Type, IDictionary<string, Action<TValue>>>(
                    new KvpOf<Type, IDictionary<string, Action<TValue>>>(
                        typeof(ApplicationUser),
                        () => new MapOf<Action<TValue>>(
                            new KvpOf<Action<TValue>>("mail", val => (entity as ApplicationUser).Email = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("pseudonym", val => (entity as ApplicationUser).Pseudonym = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("pseudonumber", val => (entity as ApplicationUser).PseudoNumber = Cast<int>(name, val)),
                            new KvpOf<Action<TValue>>("points", val => (entity as ApplicationUser).Points = Cast<double>(name, val)),
                            new KvpOf<Action<TValue>>("goodscore", val => (entity as ApplicationUser).GoodScore = Cast<double>(name, val)),
                            new KvpOf<Action<TValue>>("balancescore", val => (entity as ApplicationUser).BalanceScore = Cast<double>(name, val)),
                            new KvpOf<Action<TValue>>("picture", val => (entity as ApplicationUser).Picture = Cast<byte[]>(name, val)),
                            new KvpOf<Action<TValue>>("friends", val => (entity as ApplicationUser).Friends = Cast<string>(name, val))
                        )
                    ),
                    new KvpOf<Type, IDictionary<string, Action<TValue>>>(
                        typeof(DbFellowship),
                        () => new MapOf<Action<TValue>>(
                            new KvpOf<Action<TValue>>("name", val => (entity as DbFellowship).Name = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("type", val => (entity as DbFellowship).Type = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("picture", val => (entity as DbFellowship).Picture = Cast<byte[]>(name, val))
                        )
                    ),
                    new KvpOf<Type, IDictionary<string, Action<TValue>>>(
                        typeof(DbTransaction),
                        () => new MapOf<Action<TValue>>(
                            new KvpOf<Action<TValue>>("title", val => (entity as DbTransaction).Title = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("initiator", val => (entity as DbTransaction).Initiator = tuser(Cast<string>(name, val))),
                            new KvpOf<Action<TValue>>("type", val => (entity as DbTransaction).Type = Cast<string>(name, val)),
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
                            new KvpOf<Action<TValue>>("owner", val => (entity as DbMembership).Owner = tuser(Cast<string>(name, val))),
                            new KvpOf<Action<TValue>>("team", val => (entity as DbMembership).Team = tfellowship(Cast<string>(name, val))),
                            new KvpOf<Action<TValue>>("share", val => (entity as DbMembership).Share = Cast<double>(name, val)),
                            new KvpOf<Action<TValue>>("role", val => (entity as DbMembership).Role = Cast<string>(name, val))
                        )
                    ),
                    new KvpOf<Type, IDictionary<string, Action<TValue>>>(
                        typeof(DbQuest), () =>
                        new MapOf<Action<TValue>>(
                            new KvpOf<Action<TValue>>("applicant", val => (entity as DbQuest).Applicant = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("apply-date", val => (entity as DbQuest).ApplyDate = Cast<DateTime>(name, val)),
                            new KvpOf<Action<TValue>>("category", val => (entity as DbQuest).Category = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("completion-time", val => (entity as DbQuest).CompletionTime = Cast<double>(name, val)),
                            new KvpOf<Action<TValue>>("contact", val => (entity as DbQuest).Contact = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("contact-needed", val => (entity as DbQuest).HasContact = Cast<bool>(name, val)),
                            new KvpOf<Action<TValue>>("description", val => (entity as DbQuest).Description = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("end-date", val => (entity as DbQuest).EndDate = Cast<DateTime>(name, val)),
                            new KvpOf<Action<TValue>>("finish-date", val => (entity as DbQuest).FinishDate = Cast<DateTime>(name, val)),
                            new KvpOf<Action<TValue>>("has-end-date", val => (entity as DbQuest).HasEndDate = Cast<bool>(name, val)),
                            new KvpOf<Action<TValue>>("issuer", val => (entity as DbQuest).Issuer = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("location", val => (entity as DbQuest).Location = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("location-needed", val => (entity as DbQuest).HasLocation = Cast<bool>(name, val)),
                            new KvpOf<Action<TValue>>("note", val => (entity as DbQuest).Note = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("picture-data", val => (entity as DbQuest).PictureData = Cast<byte[]>(name, val)),
                            new KvpOf<Action<TValue>>("picture-type", val => (entity as DbQuest).PictureType = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("picture-url", val => (entity as DbQuest).PictureUrl = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("reward", val => (entity as DbQuest).Reward = Cast<double>(name, val)),
                            new KvpOf<Action<TValue>>("scope", val => (entity as DbQuest).Scope = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("status", val => (entity as DbQuest).Status = Cast<string>(name, val)),
                            new KvpOf<Action<TValue>>("title", val => (entity as DbQuest).Title = Cast<string>(name, val))
                        )
                    )
                );
            this.entity = entity;
            this.name = name;
        }

        public TEntity Invoke(TValue input)
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

            return this.entity;
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
