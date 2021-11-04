using Poof.Snaps.Outcome;
using System;

namespace Poof.Snaps.Opt
{
    /// <summary>
    /// A Optional outcome that has no content
    /// </summary>
    public sealed class OptNone<TValue> : IOpt<TValue>
    {
        public bool Has()
        {
            return false;
        }

        public IOutcome<TValue> Value()
        {
            throw new InvalidOperationException($"Cannot read a value from optional because it has none.");
        }
    }
}
