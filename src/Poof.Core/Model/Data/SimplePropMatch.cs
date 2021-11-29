using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms.Error;

namespace Poof.Core.Model.Data
{
    public abstract class PropMatchEnvelope : IPropMatch
    {
        private readonly string name;
        private readonly string type;
        private readonly object value;

        public PropMatchEnvelope(string name, string type, object value)
        {
            this.name = name;
            this.type = type;
            this.value = value;
        }

        public string Name()
        {
            return this.name;
        }

        public string Type()
        {
            return this.type;
        }

        public T Value<T>()
        {
            new FailNull(this.value,
                new InvalidOperationException($"Unable to retrieve match value of property '{name}', because it is not set.")
            ).Go();

            var result = (T)Convert.ChangeType(this.value, typeof(T));

            new FailNull(result,
                new InvalidOperationException($"Unable to cast property '{name}', because the wrong type was specified.")
            ).Go();

            return result;
        }
    }
}
