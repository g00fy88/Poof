using Poof.Core.Entity.User;
using Poof.Core.Entity.Friendship;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace Poof.Core.Snaps.User
{
    /// <summary>
    /// confirms the friendship
    /// </summary>
    public sealed class ConfirmsFriend : SnapEnvelope<IInput>
    {
        /// <summary>
        /// confirms the friendship
        /// </summary>
        public ConfirmsFriend(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var friendship = new FriendshipOf(mem, dmd.Param("friendship"));
            var userId = identity.UserID();
            if (new Friend.Of(friendship).AsString() != userId)
            {
                throw new InvalidOperationException($"Unable to confirm friendship, because the requesting user is not in the friend role.");
            }
            friendship.Update(new Status("confirmed"));

            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
