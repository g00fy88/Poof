using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Core.Model
{
    public sealed class FkIdentity : IIdentity
    {
        private readonly string id;

        public FkIdentity() : this("fake-id")
        { }

        public FkIdentity(string id)
        {
            this.id = id;
        }

        public string UserID()
        {
            return this.id;
        }
    }
}
