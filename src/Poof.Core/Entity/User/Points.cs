using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model;
using Poof.Core.Model.Data;
using Poof.Core.Model.Entity;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.User
{
    /// <summary>
    /// The points of the user
    /// </summary>
    public sealed class Points : EntityInputEnvelope
    {
        /// <summary>
        /// The points of the user
        /// </summary>
        /// <param name="points">adds up to the current points of the user</param>
        public Points(double points) : base(mem =>
            mem.Update("points", mem.Prop<double>("points") + points)
        )
        { }

        /// <summary>
        /// The points of the user
        /// </summary>
        public sealed class Of : ScalarEnvelope<double>
        {
            /// <summary>
            /// The points of the user
            /// </summary>
            public Of(IEntity user) : base(()=>
                user.Memory().Prop<double>("points")
            )
            { }
        }

        /// <summary>
        /// The give-factor of the user, which is correlated to the points.
        /// A give factor of 1.0 means, that the user needs to give (so receive points),
        /// to get his point score into equilibrium (-> 0).
        /// A give factor of 0.0, means, that the user should stop to give, 
        /// because he propably has too much points
        /// </summary>
        public sealed class GiveFactor : ScalarEnvelope<double>
        {
            /// <summary>
            /// The give-factor of the user, which is correlated to the points.
            /// A give factor of 1.0 means, that the user needs to give (so receive points),
            /// to get his point score into equilibrium (-> 0).
            /// A give factor of 0.0, means, that the user should stop to give, 
            /// because he propably has too much points
            /// </summary>
            public GiveFactor(IEntity user) : base(()=>
            {
                Func<double, double> giveFunction = p =>
                {
                    var result = 0.5;
                    if(p>0 && p<400)
                    {
                        result = 0.5 - p / 800;
                    }
                    else if(p >= 400)
                    {
                        result = 0;
                    }
                    return result;
                };

                return giveFunction(new Of(user).Value());
                    
            })
            { }
        }

        /// <summary>
        /// The take-factor of the user, which is correlated to the points.
        /// A take factor of 1.0 means, that the user needs to take (so give points),
        /// to get his point score into equilibrium (-> 0).
        /// A take factor of 0.0, means, that the user should stop to take, 
        /// because he propably has a high minus score already.
        /// </summary>
        public sealed class TakeFactor : ScalarEnvelope<double>
        {
            /// <summary>
            /// The take-factor of the user, which is correlated to the points.
            /// A take factor of 1.0 means, that the user needs to take (so give points),
            /// to get his point score into equilibrium (-> 0).
            /// A take factor of 0.0, means, that the user should stop to take, 
            /// because he propably has a high minus score already.
            /// </summary>
            public TakeFactor(IEntity user) : base(() =>
            {
                Func<double, double> takeFunction = p =>
                {
                    var result = 0.0;
                    if (p > -400 && p < 0)
                    {
                        result = 0.5 + p / 800;
                    }
                    else if (p >= 0)
                    {
                        result = 0.5;
                    }
                    return result;
                };

                return takeFunction(new Of(user).Value());

            })
            { }
        }
    }
}
