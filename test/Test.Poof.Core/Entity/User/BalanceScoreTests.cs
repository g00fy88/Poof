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

        [Fact]
        public void ReturnsLevel()
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            user.Update(new BalanceScore(12));
            user.Update(new BalanceScore(8));

            Assert.Equal(
                2.75,
                new BalanceScore.Level(user).Value()
            );
        }

        [Fact]
        public void CannotDescendLevel()
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            user.Update(new BalanceScore(12));
            user.Update(new BalanceScore(29));
            user.Update(new BalanceScore(-30));

            Assert.Equal(
                4.0,
                new BalanceScore.Level(user).Value(),
                4
            );
        }
    }
}
