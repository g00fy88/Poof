namespace Poof.Snaps
{
    /// <summary>
    /// Optional type.
    /// Use as a return value to express if a value is there or not.
    /// </summary>
    public interface IOpt<TValue>
    {
        bool Has();
        IOutcome<TValue> Value();
    }
}
