using Poof.Core.Model.Entity;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.User
{
    /// <summary>
    /// The mail address of the user
    /// </summary>
    public sealed class Mail : EntityInputEnvelope
    {
        /// <summary>
        /// The mail address of the user
        /// </summary>
        public Mail(string address) : base(mem =>
            mem.Update("mail", address)
        )
        { }

        /// <summary>
        /// The mail address of the user
        /// </summary>
        public sealed class Of : TextEnvelope
        {
            /// <summary>
            /// The mail address of the user
            /// </summary>
            public Of(IEntity user) : base(()=>
                user.Memory().Prop<string>("mail"),
                false
            )
            { }
        }
    }
}
