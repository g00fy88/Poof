using Poof.DB.Test;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Poof.Core.Entity.User.Test
{
    public sealed class PointsTests
    {
        [Fact]
        public void HasZeroPointsByDefault()
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            Assert.Equal(
                0,
                new Points.Of(user).Value()
            );
        }

        [Fact]
        public void AddsUpPoints()
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            user.Update(new Points(12));
            user.Update(new Points(9));

            Assert.Equal(
                21,
                new Points.Of(user).Value()
            );
        }

        [Theory]
        [InlineData(-200, 0.5)]
        [InlineData(0, 0.5)]
        [InlineData(200, 0.25)]
        [InlineData(400, 0)]
        [InlineData(600, 0)]
        public void RetrievesGiveFactor(double points, double expected)
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            user.Update(new Points(points));

            Assert.Equal(
                expected,
                new Points.GiveFactor(user).Value()
            );
        }

        [Theory]
        [InlineData(-600, 0)]
        [InlineData(-400, 0)]
        [InlineData(-200, 0.25)]
        [InlineData(0, 0.5)]
        [InlineData(200, 0.5)]
        public void RetrievesTakeFactor(double points, double expected)
        {
            var mem = new TestBuilding();
            var user =
                new UserOf(
                    mem,
                    new Users(mem).New()
                );

            user.Update(new Points(points));

            Assert.Equal(
                expected,
                new Points.TakeFactor(user).Value()
            );
        }
    }
}
