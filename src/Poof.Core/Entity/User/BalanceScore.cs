﻿using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model.Entity;
using Yaapii.Atoms.Scalar;
using Yaapii.Atoms.Text;

namespace Poof.Core.Entity.User
{
    /// <summary>
    /// The score of the user.
    /// Inidicates the activity of giving of this user
    /// and also how well he acts in equilibrium (take/give).
    /// </summary>
    public sealed class BalanceScore : EntityInputEnvelope
    {
        /// <summary>
        /// The score of the user.
        /// Inidicates the activity of giving of this user
        /// and also how well he acts in equilibrium (take/give).
        /// </summary>
        public BalanceScore(double newAmount) : base(mem =>
            mem.Update(
                $"balancescore",
                mem.Prop<double>("balancescore") + newAmount
            )
        )
        { }

        /// <summary>
        /// The score of the user.
        /// Inidicates the activity of giving of this user
        /// and also how well he acts in equilibrium (take/give).
        /// </summary>
        public sealed class Total : ScalarEnvelope<double>
        {
            /// <summary>
            /// The score of the user.
            /// Inidicates the activity of giving of this user
            /// and also how well he acts in equilibrium (take/give).
            /// </summary>
            public Total(IEntity user) : base(()=>
                user.Memory().Prop<double>("balancescore")
            )
            { }
        }
    }
}
