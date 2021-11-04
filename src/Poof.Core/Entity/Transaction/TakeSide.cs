using Poof.Core.Model.Entity;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Transaction
{
    /// <summary>
    /// The taking side in the transaction,
    /// who takes something and gives points.
    /// Can be of type 'fellowship' or 'user'
    /// </summary>
    public sealed class TakeSide : EntityInputEnvelope
    {
        /// <summary>
        /// The taking side in the transaction,
        /// who takes something and gives points.
        /// Can be of type 'fellowship' or 'user'
        /// </summary>
        public TakeSide(string type, string entity) : base(mem =>
        {
            mem.Update("takeside", entity);
            mem.Update("taketype", new Strict(type, "fellowship", "user").AsString());
        })
        { }

        /// <summary>
        /// The taking side in the transaction,
        /// who takes something and gives points.
        /// Can be of type 'fellowship' or 'user'
        /// </summary>
        public sealed class Type : TextEnvelope
        {
            /// <summary>
            /// The taking side in the transaction,
            /// who takes something and gives points.
            /// Can be of type 'fellowship' or 'user'
            /// </summary>
            public Type(IEntity transaction) : base(() =>
                transaction.Memory().Prop<string>("taketype"),
                false
            )
            { }
        }

        /// <summary>
        /// The taking side in the transaction,
        /// who takes something and gives points.
        /// Can be of type 'fellowship' or 'user'
        /// </summary>
        public sealed class Entity : TextEnvelope
        {
            /// <summary>
            /// The taking side in the transaction,
            /// who takes something and gives points.
            /// Can be of type 'fellowship' or 'user'
            /// </summary>
            public Entity(IEntity transaction) : base(() =>
                transaction.Memory().Prop<string>("takeside"),
                false
            )
            { }
        }
    }
}
