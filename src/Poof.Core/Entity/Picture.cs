using Poof.Core.Model.Entity;
using System.IO;
using Yaapii.Atoms;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity
{
    /// <summary>
    /// The picture stored in the entity
    /// </summary>
    public sealed class Picture : EntityInputEnvelope
    {
        /// <summary>
        /// The picture stored in the entity
        /// </summary>
        public Picture(IInput picture) : base(mem =>
            mem.Update("picture", new BytesOf(picture).AsBytes())
        )
        { }

        /// <summary>
        /// The picture stored in the entity
        /// </summary>
        public sealed class Of : IInput
        {
            private readonly IScalar<byte[]> pic;

            /// <summary>
            /// The picture stored in the entity
            /// </summary>
            public Of(IEntity entity)
            {
                this.pic =
                    new ScalarOf<byte[]>(() =>
                        entity.Memory().Prop<byte[]>("picture")
                    );
            }

            public Stream Stream()
            {
                return new InputOf(this.pic.Value()).Stream();
            }
        }

        public sealed class Base64Url : TextEnvelope
        {
            public Base64Url(IEntity entity) : base(()=>
                new TextOf(
                    new InputOf(
                        new BytesBase64(
                            new BytesOf(entity.Memory().Prop<byte[]>("picture"))
                        )
                    )
                ).AsString(),
                false
            )
            { }
        }
    }
}
