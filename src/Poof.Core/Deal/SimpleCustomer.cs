using Poof.Core.Model.Deal;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Deal
{
    public sealed class SimpleCustomer : ICustomer
    {
        private readonly string type;
        private readonly string id;

        public SimpleCustomer(string type, string id)
        {
            this.type = type;
            this.id = id;
        }

        public string ID()
        {
            return this.id;
        }

        public string Type()
        {
            return new Strict(this.type, "user", "fellowship").AsString();
        }
    }
}
