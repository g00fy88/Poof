using Poof.Core.Entity.Facets;
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
                var totalPoints = 0.0;
                foreach (var id in memberships)
                {
                    var membership = new MembershipOf(mem, id);
                    var share = new Share.Of(membership).Value();
                    totalShare += share;
                    totalPoints += 
                        share * 
                        new Points.Of(
                            new UserOf(mem, 
                                new Owner.Of(membership).AsString()
                            )
                        ).Value();
                }

                return new GiveFactor().Invoke(totalPoints / totalShare);
            })
            { }
        }

        public sealed class Take : ScalarEnvelope<double>
        {
            public Take(IDataBuilding mem, string fellowship) : base(() =>
            {
                var memberships = new Memberships(mem).List(new Team.Match(fellowship));

                var totalShare = 0.0;
                var totalPoints = 0.0;
                foreach (var id in memberships)
                {
                    var membership = new MembershipOf(mem, id);
                    var share = new Share.Of(membership).Value();
                    totalShare += share;
                    totalPoints += 
                        share * 
                        new Points.Of(
                            new UserOf(mem, 
                                new Owner.Of(membership).AsString()
                            )
                        ).Value();
                }

                return new TakeFactor().Invoke(totalPoints / totalShare);
            })
            { }
        }
    }
}
