using Poof.Core.Entity.User;
using Poof.Core.Model.Data;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Scalar;

namespace Poof.Core.Snaps.User.Facets
{
    public sealed class NewPseudonumber : ScalarEnvelope<int>
    {
        public NewPseudonumber(IDataBuilding mem, string pseudoname) : base(()=>
        {
            var existentNumbers =
                new Yaapii.Atoms.List.Mapped<string, int>(id =>
                    new Pseudonym.Number(new UserOf(mem, id)).Value(),
                    new Users(mem).List(new Pseudonym.Match(pseudoname))
                );
            var random = new Random();
            var number = random.Next(0, 10000);
            while (existentNumbers.Contains(number))
            {
                number = random.Next(0, 10000);
            }
            return number;
        })
        { }
    }
}
