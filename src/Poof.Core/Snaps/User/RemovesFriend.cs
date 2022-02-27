using Poof.Core.Entity.Friendship;
using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;

namespace Poof.Core.Snaps.User
{
    public sealed class RemovesFriend : SnapEnvelope<IInput>
    {
        public RemovesFriend(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var userId = identity.UserID();
            var toRemove = dmd.Param("friendship");
            var friendship = new FriendshipOf(mem, toRemove);
            if(new Requester.Of(friendship).AsString() != userId && new Friend.Of(friendship).AsString() != userId)
            {
                throw new InvalidOperationException($"Unable to remove friendship, because the requested user is not part of it.");
            }
            new Friendships(mem).Remove(toRemove);

            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
