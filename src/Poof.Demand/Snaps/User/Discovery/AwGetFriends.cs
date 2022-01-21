using Poof.Snaps;
using Yaapii.Atoms;
using Yaapii.Atoms.List;
using Yaapii.JSON;

namespace Poof.Talk.Snaps.User.Discovery
{
    public sealed class AwGetFriends
    {
        public sealed class List : ListEnvelope<IJSON>
        {
            public List(IOutcome<IInput> outcome) : base(()=>
                new JSONOf(outcome.Result()).Nodes("[*]"),
                false
            )
            { }
        }
    }
}
