using Poof.Core.Model.Deal;
using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Text;

namespace Poof.Core.Deal
{
    public sealed class SimpleDealer : IDealer
    {
        private readonly string type;
        private readonly string id;
        private readonly double amount;

        public SimpleDealer(string type, string id, double amount)
        {
            this.type = type;
            this.id = id;
            this.amount = amount;
        }

        public string ID()
        {
            return this.id;
        }

        public double Points()
        {
            return this.amount;
        }

        public string Type()
        {
            return new Strict(this.type, "user", "fellowship").AsString();
        }
    }
}
