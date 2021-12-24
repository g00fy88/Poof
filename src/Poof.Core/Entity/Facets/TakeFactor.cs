using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;

namespace Poof.Core.Entity.Facets
{
    /// <summary>
    /// The take-factor of the user, which is correlated to the points.
    /// A take factor of 1.0 means, that the user needs to take (so give points),
    /// to get his point score into equilibrium (-> 0).
    /// A take factor of 0.0, means, that the user should stop to take, 
    /// because he propably has a high minus score already.
    /// </summary>
    internal sealed class TakeFactor : IFunc<double, double>
    {
        public double Invoke(double p)
        {
            var result = -0.5;
            if (p > -800 && p < 0)
            {
                result = 0.5 + p / 800;
            }
            else if (p >= 0)
            {
                result = 0.5;
            }
            return result;
        }
    }
}
