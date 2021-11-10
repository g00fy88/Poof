using Poof.Core.Entity.User;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Snaps;
using Poof.Snaps.Outcome;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;
using Yaapii.Atoms.Enumerable;

namespace Poof.Core.Snaps.User.Configuration
{
    public sealed class UpdatesUserData : SnapEnvelope<IInput>
    {
        public UpdatesUserData(IDataBuilding mem, IIdentity identity) : base(dmd =>
        {
            var pseudonym = dmd.Param("pseudonym");

            var existentNumbers =
                new Yaapii.Atoms.List.Mapped<string, int>(id =>
                    new Pseudonym.Number(new UserOf(mem, id)).Value(),
                    new Filtered<string>(
                        id => new Pseudonym.Name(new UserOf(mem, id)).AsString().Equals(pseudonym, StringComparison.OrdinalIgnoreCase),
                        new Users(mem).List()
                    )
                );
            var random = new Random();
            var number = random.Next(0, 10000);
            while (existentNumbers.Contains(number))
            {
                number = random.Next(0, 10000);
            }

            new UserOf(mem, identity.UserID()).Update(
                new Pseudonym(pseudonym, number)
            );

            return new EmptyOutcome<IInput>();
        })
        { }
    }
}
