using Poof.Core.Entity.Membership;
using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Fellowship.Test
{
    public sealed class FactorTests
    {
        [Fact]
        public void CalculatesGiveFactor()
        {
            var mem = new TestBuilding();
            var fellowship = new Fellowships(mem).New();
            
            var user1 = new Users(mem).New();
            var user2 = new Users(mem).New();
            new UserOf(mem, user1).Update(new Points(400));

            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Owner(user1),
                new Team(fellowship),
                new Share(1)
            );
            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Owner(user2),
                new Team(fellowship),
                new Share(1)
            );

            Assert.Equal(
                0.25,
                new Factor.Give(mem, fellowship).Value()
            );
        }

        [Fact]
        public void CalculatesTakeFactor()
        {
            var mem = new TestBuilding();
            var fellowship = new Fellowships(mem).New();

            var user1 = new Users(mem).New();
            var user2 = new Users(mem).New();
            new UserOf(mem, user1).Update(new Points(-400));

            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Owner(user1),
                new Team(fellowship),
                new Share(1)
            );
            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Owner(user2),
                new Team(fellowship),
                new Share(1)
            );

            Assert.Equal(
                0.25,
                new Factor.Take(mem, fellowship).Value()
            );
        }
    }
}
