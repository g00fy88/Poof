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
            var user = new UserOf(mem, userId);
            var currentFriends = new List<string>(new Friends.Of(user));
            var toRemove = dmd.Param("friend");

            if(currentFriends.Contains(toRemove))
            {
                currentFriends.Remove(toRemove);
                user.Update(new Friends(currentFriends));
            }

            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
