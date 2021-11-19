using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;

namespace Poof.Core.Entity.User
{
    public sealed class Friends : EntityInputEnvelope
    {
        public Friends(params string[] users) : this(
            new ManyOf(users)
        )
        { }

        public Friends(IEnumerable<string> users, string newUser) : this(
            new Yaapii.Atoms.Enumerable.Joined<string>(users, newUser)
        )
        { }

        public Friends(IEnumerable<string> users) : base(floor =>
            floor.Update("friends", users.ToArray())
        )
        { }

        public sealed class Of : ListEnvelope<string>
        {
            public Of(IEntity user) : base(()=>
                user.Memory().Prop<string[]>("friends"),
                false
            )
            { }
        }
    }
}
