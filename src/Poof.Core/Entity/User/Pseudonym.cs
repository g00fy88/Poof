using Poof.Core.Model.Entity;
using Yaapii.Atoms.Scalar;
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
        public Pseudonym(string name, int number) : base(mem =>
        {
            mem.Update("pseudonym", name);
            mem.Update("pseudonumber", number);
        })
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

        /// <summary>
        /// The pseudonym name of the user
        /// </summary>
        public sealed class Number : ScalarEnvelope<int>
        {
            /// <summary>
            /// The pseudonym name of the user
            /// </summary>
            public Number(IEntity user) : base(() =>
                user.Memory().Prop<int>("pseudonumber")
            )
            { }
        }
    }
}
