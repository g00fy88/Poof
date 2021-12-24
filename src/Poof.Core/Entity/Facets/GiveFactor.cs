using System;
using System.Collections.Generic;
using System.Text;
using Yaapii.Atoms;

namespace Poof.Core.Entity.Facets
{
    /// <summary>
    /// The give-factor of the user, which is correlated to the points.
    /// A give factor of 1.0 means, that the user needs to give (so receive points),
    /// to get his point score into equilibrium (-> 0).
    /// A give factor of 0.0, means, that the user should stop to give, 
    /// because he propably has too much points
    /// </summary>
    internal sealed class GiveFactor : IFunc<double, double>
    {
        public double Invoke(double p)
        {
            var result = 0.5;
            if (p > 0 && p < 800)
            {
                result = 0.5 - p / 800;
            }
            else if (p >= 800)
            {
                result = 0;
            }
            return result;
        }
    }
}
