using Poof.Core.Entity.Membership;
using Poof.Core.Entity.User;
using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Poof.Core.Entity.Fellowship
{
    public sealed class Factor
    {
        public sealed class Give : ScalarEnvelope<double>
        {
            public Give(IDataBuilding mem, string fellowship) : base(()=>
            {
                var memberships = new Memberships(mem).List(new Team.Match(fellowship));

                var totalShare = 0.0;
                var totalFactor = 0.0;
                foreach (var id in memberships)
                {
                    var membership = new MembershipOf(mem, id);
                    var share = new Share.Of(membership).Value();
                    totalShare += share;
                    totalFactor += 
                        share * 
                        new Points.GiveFactor(
                            new UserOf(mem, 
                                new Owner.Of(membership).AsString()
                            )
                        ).Value();
                }

                return totalFactor / totalShare;
            })
            { }
        }

        public sealed class Take : ScalarEnvelope<double>
        {
            public Take(IDataBuilding mem, string fellowship) : base(() =>
            {
                var memberships = new Memberships(mem).List(new Team.Match(fellowship));

                var totalShare = 0.0;
                var totalFactor = 0.0;
                foreach (var id in memberships)
                {
                    var membership = new MembershipOf(mem, id);
                    var share = new Share.Of(membership).Value();
                    totalShare += share;
                    totalFactor += 
                        share * 
                        new Points.TakeFactor(
                            new UserOf(mem, 
                                new Owner.Of(membership).AsString()
                            )
                        ).Value();
                }

                return totalFactor / totalShare;
            })
            { }
        }
    }
}
