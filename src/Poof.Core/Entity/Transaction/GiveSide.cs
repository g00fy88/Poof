using Poof.Core.Model.Entity;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Transaction
{
    /// <summary>
    /// The giving side in the transaction,
    /// who gives something and receives points.
    /// Can be of type 'fellowship' or 'user'
    /// </summary>
    public sealed class GiveSide : EntityInputEnvelope
    {
        /// <summary>
        /// The giving side in the transaction,
        /// who gives something and receives points.
        /// Can be of type 'fellowship' or 'user'
        /// </summary>
        public GiveSide(string type, string entity) : base(mem =>
        {
            mem.Update("giveside", entity);
            mem.Update("givetype", new Strict(type, "fellowship", "user").AsString());
        })
        { }

        /// <summary>
        /// The giving side in the transaction,
        /// who gives something and receives points.
        /// Can be of type 'fellowship' or 'user'
        /// </summary>
        public sealed class Type : TextEnvelope
        {
            /// <summary>
            /// The giving side in the transaction,
            /// who gives something and receives points.
            /// Can be of type 'fellowship' or 'user'
            /// </summary>
            public Type(IEntity transaction) : base(()=>
                transaction.Memory().Prop<string>("givetype"),
                false
            )
            { }
        }

        /// <summary>
        /// The giving side in the transaction,
        /// who gives something and receives points.
        /// Can be of type 'fellowship' or 'user'
        /// </summary>
        public sealed class Entity : TextEnvelope
        {
            /// <summary>
            /// The giving side in the transaction,
            /// who gives something and receives points.
            /// Can be of type 'fellowship' or 'user'
            /// </summary>
            public Entity(IEntity transaction) : base(() =>
                transaction.Memory().Prop<string>("giveside"),
                false
            )
            { }
        }
    }
}
