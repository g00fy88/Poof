using System;
using System.Collections.Generic;
using System.Text;

namespace Poof.Talk
{
    public interface IMemory
    {
        string Prop(string name, string fallback);

        void Update(string name, string value);

        byte[] Content(string name, byte[] fallback);

        void Update(string name, byte[] value);

    }
}
