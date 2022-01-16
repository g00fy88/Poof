using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk.Memory
{
    public sealed class MemoryOf : IMemory
    {
        private readonly ConcurrentDictionary<string, string> props;
        private readonly ConcurrentDictionary<string, byte[]> contents;

        public MemoryOf()
        {
            this.props = new ConcurrentDictionary<string, string>();
            this.contents = new ConcurrentDictionary<string, byte[]>();
        }

        public byte[] Content(string name, byte[] fallback)
        {
            return this.contents.GetValueOrDefault(name, fallback);
        }

        public string Prop(string name, string fallback)
        {
            return this.props.GetValueOrDefault(name, fallback);
        }

        public void Update(string name, string value)
        {
            this.props.AddOrUpdate(name, value, (key, v) => value);
        }

        public void Update(string name, byte[] value)
        {
            this.contents.AddOrUpdate(name, value, (key, v) => value);
        }
    }
}
