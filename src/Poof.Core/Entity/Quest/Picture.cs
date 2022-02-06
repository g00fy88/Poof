using Poof.Core.Model.Entity;
using System;
using System.IO;
using Yaapii.Atoms;
using Yaapii.Atoms.Bytes;
using Yaapii.Atoms.IO;
using Yaapii.Atoms.Map;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.Quest
{
    /// <summary>
    /// The picture stored in the quest
    /// </summary>
    public sealed class Picture : EntityInputEnvelope
    {
        /// <summary>
        /// The picture stored in the quest
        /// </summary>
        public Picture(IInput picture) : base(mem =>
        {
            mem.Update("picture-type", "bytes");
            mem.Update("picture-data", new BytesOf(picture).AsBytes());
        })
        { }

        /// <summary>
        /// The picture stored in the quest
        /// </summary>
        public Picture(string pictureUrl) : base(mem =>
        {
            mem.Update("picture-type", "url");
            mem.Update("picture-url", pictureUrl);
        })
        { }

        /// <summary>
        /// The picture stored in the quest
        /// </summary>
        public sealed class Has : ScalarEnvelope<bool>
        {
            /// <summary>
            /// The picture stored in the quest
            /// </summary>
            public Has(IEntity quest) : base(()=>
                quest.Memory().Prop<string>("picture-type") != "none"
            )
            { }
        }

        /// <summary>
        /// The picture stored in the quest
        /// </summary>
        public sealed class Url : TextEnvelope
        {
            /// <summary>
            /// The picture stored in the quest
            /// </summary>
            public Url(IEntity quest) : base(() =>
                new FallbackMap(
                    new MapOf(
                        new KvpOf("bytes", ()=>
                            new TextOf(
                                new InputOf(
                                    new BytesBase64(
                                        new BytesOf(quest.Memory().Prop<byte[]>("picture-data"))
                                    )
                                )
                            ).AsString()
                        ),
                        new KvpOf("url", ()=>
                            quest.Memory().Prop<string>("picture-url")
                        )
                    ),
                    key => throw new InvalidOperationException($"Unable to retrieve picture url, because the picture type '{key}' is not supported.")
                )[quest.Memory().Prop<string>("picture-type")],
                false
            )
            { }
        }
    }
}
