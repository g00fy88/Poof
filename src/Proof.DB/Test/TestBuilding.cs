﻿using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.DB.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;

namespace Poof.DB.Test
{
    public sealed class TestBuilding : IDataBuilding
    {
        private readonly string name;
        private readonly IList<ApplicationUser> users;
        private readonly IList<DbFellowship> fellowships;
        private readonly IList<DbTransaction> transactions;
        private readonly IList<DbMembership> memberships;

        public TestBuilding() : this(
            "",
            new List<ApplicationUser>(),
            new List<DbFellowship>(),
            new List<DbTransaction>(),
            new List<DbMembership>()
        )
        { }

        private TestBuilding(string name, IList<ApplicationUser> users, IList<DbFellowship> fellowships, IList<DbTransaction> transactions, IList<DbMembership> memberships)
        {
            this.name = name;
            this.users = users;
            this.fellowships = fellowships;
            this.transactions = transactions;
            this.memberships = memberships;
        }

        public void Add(string floor)
        {
            new FallbackMap<Action>(
                new MapOf<Action>(
                    new KvpOf<Action>("user", () => this.users.Add(new ApplicationUser() { Id = floor })),
                    new KvpOf<Action>("fellowship", () => this.fellowships.Add(new DbFellowship() { Id = floor })),
                    new KvpOf<Action>("transaction", () => this.transactions.Add(new DbTransaction() { Id = floor })),
                    new KvpOf<Action>("membership", () => this.memberships.Add(new DbMembership() { Id = floor }))
                ),
                key => throw new InvalidOperationException($"Unable to add new entity, because type '{key}' is unknown.")
            )[this.name].Invoke();
        }

        public IDataFloor Floor(string id)
        {
            return
                new FallbackMap<IDataFloor>(
                    new MapOf<IDataFloor>(
                        new KvpOf<IDataFloor>("user", () => new TestUserFloor(User(id))),
                        new KvpOf<IDataFloor>("fellowship", () => new TestFellowshipFloor(Fellowship(id))),
                        new KvpOf<IDataFloor>("transaction", () =>new TestTransactionFloor(Transaction(id))),
                        new KvpOf<IDataFloor>("membership", () => new TestMembershipFloor(Membership(id), this.users, this.fellowships))
                   ),
                   key => throw new InvalidOperationException($"Unable to add new entity, because type '{key}' is unknown.")
               )[this.name];
        }

        public IList<string> Floors(params IPropMatch[] matches)
        {
            return
                new FallbackMap<IList<string>>(
                    new MapOf<IList<string>>(
                        new KvpOf<IList<string>>("user", () => new Yaapii.Atoms.List.Mapped<ApplicationUser, string>(user => user.Id, this.users)),
                        new KvpOf<IList<string>>("fellowship", () => new Yaapii.Atoms.List.Mapped<DbFellowship, string>(fellowships => fellowships.Id, this.fellowships)),
                        new KvpOf<IList<string>>("transaction", () => new Yaapii.Atoms.List.Mapped<DbTransaction, string>(transactions => transactions.Id, this.transactions)),
                        new KvpOf<IList<string>>("membership", () => new Yaapii.Atoms.List.Mapped<DbMembership, string>(membership => membership.Id, this.memberships))
                   ),
                   key => throw new InvalidOperationException($"Unable to add new entity, because type '{key}' is unknown.")
               )[this.name];
        }

        public IDataBuilding Moved(string name)
        {
            return
                new TestBuilding(
                    name,
                    this.users,
                    this.fellowships,
                    this.transactions,
                    this.memberships
                );
        }

        public void Remove(string floor)
        {
            new FallbackMap<Action>(
                new MapOf<Action>(
                    new KvpOf<Action>("user", () => this.users.Remove(User(floor))),
                    new KvpOf<Action>("fellowship", () => this.fellowships.Remove(Fellowship(floor))),
                    new KvpOf<Action>("transaction", () => this.transactions.Remove(Transaction(floor))),
                    new KvpOf<Action>("membership", () => this.memberships.Remove(Membership(floor)))
                ),
                key => throw new InvalidOperationException($"Unable to add new entity, because type '{key}' is unknown.")
            )[this.name].Invoke();
        }

        private ApplicationUser User(string id)
        {
            return
                Single(
                    new Filtered<ApplicationUser>(
                        user => user.Id == id,
                        this.users
                    )
                );
        }

        private DbTransaction Transaction(string id)
        {
            return
                Single(
                    new Filtered<DbTransaction>(
                        user => user.Id == id,
                        this.transactions
                    )
                );
        }

        private DbFellowship Fellowship(string id)
        {
            return
                Single(
                    new Filtered<DbFellowship>(
                        user => user.Id == id,
                        this.fellowships
                    )
                );
        }

        private DbMembership Membership(string id)
        {
            return
                Single(
                    new Filtered<DbMembership>(
                        user => user.Id == id,
                        this.memberships
                    )
                );
        }

        private T Single<T>(IEnumerable<T> list)
        {
            if(new LengthOf(list).Value() != 1)
            {
                throw new InvalidOperationException("Unable to retrieve entity, because it does not exist, or exists more than once.");
            }
            return new FirstOf<T>(list).Value();
        }
    }
}
