using Poof.Core.Model.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yaapii.Atoms.Enumerable;
using Yaapii.Atoms.List;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

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
            floor.Update("friends", new Yaapii.Atoms.Text.Joined(";", users).AsString())
        )
        { }

        public sealed class Of : ListEnvelope<string>
        {
            public Of(IEntity user) : base(
                new ScalarOf<IEnumerable<string>>(()=>
                    new Split(
                        user.Memory().Prop<string>("friends"),
                        ";"
                    )
                ),
                false
            )
            { }
        }
    }
}
