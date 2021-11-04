namespace Poof.Snaps
{
    /// <summary>
    /// Routes a message. 
    /// Facets of this type look into specifics of the request and return a positive
    /// output if they match, or a negative output if they don't.
    /// </summary>
    public interface IFlow<TResult>
    {
        IOpt<TResult> Response(IDemand demand);
    }
}
