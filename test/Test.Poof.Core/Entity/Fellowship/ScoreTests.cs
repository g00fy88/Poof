using Poof.Core.Entity.Membership;
using Poof.Core.Entity.User;
using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.Fellowship.Test
{
    public sealed class ScoreTests
    {
        [Fact]
        public void CalculatesScore()
        {
            var mem = new TestBuilding();
            var fellowship = new Fellowships(mem).New();
            
            var user1 = new Users(mem).New();
            var user2 = new Users(mem).New();
            new UserOf(mem, user1).Update(new BalanceScore(20));
            new UserOf(mem, user2).Update(new BalanceScore(10));

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
                15,
                new Score.Activity(mem, fellowship).Value()
            );
        }

        [Fact]
        public void WeightsScore()
        {
            var mem = new TestBuilding();
            var fellowship = new Fellowships(mem).New();

            var user1 = new Users(mem).New();
            var user2 = new Users(mem).New();
            new UserOf(mem, user1).Update(new BalanceScore(20));
            new UserOf(mem, user2).Update(new BalanceScore(10));

            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Owner(user1),
                new Team(fellowship),
                new Share(1)
            );
            new MembershipOf(mem, new Memberships(mem).New()).Update(
                new Owner(user2),
                new Team(fellowship),
                new Share(0.6)
            );

            Assert.Equal(
                16.25,
                new Score.Activity(mem, fellowship).Value()
            );
        }
    }
}
