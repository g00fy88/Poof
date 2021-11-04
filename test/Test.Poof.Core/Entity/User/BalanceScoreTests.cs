using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.User.Test
{
    public sealed class BalanceScoreTests
    {
        [Fact]
        public void HasZeroScoreByDefault()
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            Assert.Equal(
                0,
                new BalanceScore.Total(user).Value()
            );
        }

        [Fact]
        public void AddsUpScore()
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            user.Update(new BalanceScore(12));
            user.Update(new BalanceScore(9));

            Assert.Equal(
                21,
                new BalanceScore.Total(user).Value()
            );
        }
    }
}
