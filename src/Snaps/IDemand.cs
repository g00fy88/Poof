using System.Collections.Generic;
using Yaapii.Atoms;

namespace Poof.Snaps
{
    /// <summary>
    /// An abstraction of a Http Request
    /// </summary>
    public interface IDemand
    {
        /// <summary>
        /// lists context parameters (-> unique names)
        /// </summary>
        /// <returns></returns>
        IList<string> Params();

        /// <summary>
        /// Retrieves a specific parameter with fallback def
        /// </summary>
        /// <param name="name"></param>
        /// <param def="def"></param>
        /// <returns></returns>
        string Param(string name);

        /// <summary>
        /// Retrieves a specific parameter with fallback def
        /// </summary>
        /// <param name="name"></param>
        /// <param def="def"></param>
        /// <returns></returns>
        string Param(string name, string def);

        /// <summary>
        /// Refines a demand with an additional Parameter and value.
        /// </summary>
        /// <param name="param"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IDemand Refined(string param, string value);

        /// <summary>
        /// The original body of the http request.
        /// </summary>
        /// <returns></returns>
        IInput Body();
    }
}
