using Poof.Core.Entity.Membership;
using Poof.Core.Entity.User;
using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Poof.Core.Entity.Fellowship
{
    public sealed class Score
    {
        public sealed class ActivityLevel : ScalarEnvelope<double>
        {
            public ActivityLevel(IDataBuilding mem, string fellowship) : base(()=>
            {
                var memberships = new Memberships(mem).List(new Team.Match(fellowship));

                var totalShare = 0.0;
                var totalLevel = 0.0;
                foreach (var id in memberships)
                {
                    var membership = new MembershipOf(mem, id);
                    var share = new Share.Of(membership).Value();
                    totalShare += share;
                    totalLevel += 
                        share * 
                        new BalanceScore.Level(
                            new UserOf(mem, 
                                new Owner.Of(membership).AsString()
                            )
                        ).Value();
                }

                return totalLevel / totalShare;
            })
            { }
        }
    }
}
