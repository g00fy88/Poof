using Poof.Core.Entity.Fellowship;
using Poof.Core.Snaps.Fellowship;
using Poof.DB.Test;
using Poof.Talk.Snaps.Fellowship;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Poof.Core.Snaps.Fellowship.Test
{
    public sealed class ChecksNameAvailabilityTests
    {
        [Fact]
        public void RetrievesAvailable()
        {
            var mem = new TestBuilding();
            new FellowshipOf(mem, new Fellowships(mem).New()).Update(
                new Name("gandalf and his boys")
            );
            Assert.True(
                new AwCheckNameAvailability.Available(
                    new CheckNameAvailability(mem).Convert(
                        new DmCheckNameAvailability("aragorn and his boys")
                    )
                ).Value()
            );
        }

        [Fact]
        public void RetrievesUnavailable()
        {
            var mem = new TestBuilding();
            new FellowshipOf(mem, new Fellowships(mem).New()).Update(
                new Name("gandalf and his boys")
            );
            Assert.False(
                new AwCheckNameAvailability.Available(
                    new CheckNameAvailability(mem).Convert(
                        new DmCheckNameAvailability("gandalf and his boys")
                    )
                ).Value()
            );
        }
    }
}
