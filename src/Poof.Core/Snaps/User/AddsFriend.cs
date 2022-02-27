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
    public sealed class AddsFriend : SnapEnvelope<IInput>
    {
        public AddsFriend(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var friendships = new Friendships(mem);
            var userId = identity.UserID();
            var user = new UserOf(mem, userId);
            var currentFriends =
                new Joined<string>(
                    new Mapped<string, string>(fs =>
                        new Friend.Of(new FriendshipOf(mem, fs)).AsString(),
                        friendships.List(new Requester.Match(userId))
                    ),
                    new Mapped<string, string>(fs =>
                        new Requester.Of(new FriendshipOf(mem, fs)).AsString(),
                        friendships.List(new Friend.Match(userId))
                    )
                );
            var newFriend = dmd.Param("friend");

            if(userId == newFriend)
            {
                throw new ArgumentException("Unable to add yourself as a friend.");
            }

            if(!new Contains<string>(currentFriends, newFriend).Value())
            {
                new FriendshipOf(mem, friendships.New()).Update(
                    new Requester(userId),
                    new Friend(newFriend),
                    new Status("requested")
                );
            }

            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
