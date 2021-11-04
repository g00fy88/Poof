using System.Collections.Generic;

namespace Poof.Snaps
{
    /// <summary>
    /// Outcome of a processed usecase (snap).
    /// </summary>
    public interface IOutcome<TResult>
    {
        IList<string> Params();
        string Param(string name);
        IOutcome<TResult> Refined(string param, string value);
        bool IsEmpty();
        TResult Result();
    }
}
