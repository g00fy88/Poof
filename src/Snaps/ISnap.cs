namespace Poof.Snaps
{
    /// <summary>
    /// Snaps specific demands to specific tasks.
    /// </summary>
    public interface ISnap<TResult>
    {
        IOutcome<TResult> Convert(IDemand demand);
    }
}
