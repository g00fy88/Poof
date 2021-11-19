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
    public sealed class AddsFriend : SnapEnvelope<IInput>
    {
        public AddsFriend(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var userId = identity.UserID();
            var user = new UserOf(mem, userId);
            var currentFriends = new Friends.Of(user);
            var newFriend = dmd.Param("friend");

            if(userId == newFriend)
            {
                throw new ArgumentException("Unable to add yourself as a friend.");
            }

            if(!currentFriends.Contains(newFriend))
            {
                user.Update(new Friends(currentFriends, newFriend));
            }

            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
