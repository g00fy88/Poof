using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model
{
    public sealed class UserIdentity : IIdentity
    {
        private readonly string id;

        public UserIdentity(string id)
        {
            this.id = id;
        }

        public string UserID()
        {
            return this.id;
        }
    }
}
