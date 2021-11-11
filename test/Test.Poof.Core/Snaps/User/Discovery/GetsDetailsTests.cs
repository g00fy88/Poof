using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.DB.Test;
using Poof.Snaps;
using Poof.Talk.Snaps.User.Discovery;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Yaapii.JSON;

namespace Poof.Core.Snaps.User.Discovery.Test
{
    public sealed class GetsDetailsTests
    {
        [Fact]
        public void RetrievesPseudoname()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var user = users.New();
            new UserOf(mem, user).Update(
                new Pseudonym("batman", 10),
                new Points(123),
                new BalanceScore(20)
            );

            Assert.Equal(
                "batman",
                new AwGetDetails.PseudoName(
                    new GetsDetails(mem, new FkIdentity(user)).Convert(
                        new DmGetDetails()
                    )
                ).AsString()
            );
        }

        [Fact]
        public void RetrievesPseudonumber()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var user = users.New();
            new UserOf(mem, user).Update(
                new Pseudonym("batman", 10),
                new Points(123),
                new BalanceScore(20)
            );

            Assert.Equal(
                10,
                new AwGetDetails.PseudoNumber(
                    new GetsDetails(mem, new FkIdentity(user)).Convert(
                        new DmGetDetails()
                    )
                ).Value()
            );
        }

        [Fact]
        public void RetrievesPoints()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var user = users.New();
            new UserOf(mem, user).Update(
                new Pseudonym("batman", 10),
                new Points(123.34),
                new BalanceScore(20.3)
            );

            Assert.Equal(
                123.34,
                new AwGetDetails.Points(
                    new GetsDetails(mem, new FkIdentity(user)).Convert(
                        new DmGetDetails()
                    )
                ).Value()
            );
        }

        [Fact]
        public void RetrievesScore()
        {
            var mem = new TestBuilding();
            var users = new Users(mem);
            var user = users.New();
            new UserOf(mem, user).Update(
                new Pseudonym("batman", 10),
                new Points(123.34),
                new BalanceScore(20.3)
            );

            Assert.Equal(
                20.3,
                new AwGetDetails.Score(
                    new GetsDetails(mem, new FkIdentity(user)).Convert(
                        new DmGetDetails()
                    )
                ).Value()
            );
        }
    }
}
