using Poof.Core.Model.Entity;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.User
{
    /// <summary>
    /// The pseudonym of the user
    /// </summary>
    public sealed class Pseudonym : EntityInputEnvelope
    {
        /// <summary>
        /// The pseudonym of the user
        /// </summary>
        public Pseudonym(string name) : base(mem =>
            mem.Update("pseudonym", name)
        )
        { }

        /// <summary>
        /// The pseudonym name of the user
        /// </summary>
        public sealed class Name : TextEnvelope
        {
            /// <summary>
            /// The pseudonym name of the user
            /// </summary>
            public Name(IEntity user) : base(()=>
                user.Memory().Prop<string>("pseudonym"),
                false
            )
            { }
        }
    }
}
