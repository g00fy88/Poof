using System;
using System.Collections.Generic;
using System.Text;
using Poof.Core.Model.Entity;
using Yaapii.Atoms;
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
        {
            if(newAmount < 0)
            {
                var total = mem.Prop<double>("balancescore");
                var currentLevel = new Level(total).Value();
                var intLevel = Math.Floor(currentLevel);
                var newLevel = new Level(total + newAmount).Value();
                if(newLevel < intLevel)
                {
                    newAmount = -(10 + intLevel) * (currentLevel % 1) + double.Epsilon;
                }
            }
            mem.Update(
                $"balancescore",
                mem.Prop<double>("balancescore") + newAmount
            );
        }
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

        /// <summary>
        /// The score level as double
        /// The whole number is the level, while the rest can be seen as progress to the next level
        /// </summary>
        public sealed class Level : ScalarEnvelope<double>
        {
            /// <summary>
            /// The score level as double
            /// The whole number is the level, while the rest can be seen as progress to the next level
            /// </summary>
            public Level(IEntity user) : this(
                new Total(user)
            )
            { }

            /// <summary>
            /// The score level as double
            /// The whole number is the level, while the rest can be seen as progress to the next level
            /// </summary>
            public Level(double total) : this(
                new ScalarOf<double>(total)
            )
            { }

            /// <summary>
            /// The score level as double
            /// The whole number is the level, while the rest can be seen as progress to the next level
            /// </summary>
            public Level(IScalar<double> total) : base(()=>
            {
                var score = total.Value();
                var level = 1;
                while (score - (10 + level) > 0)
                {
                    score -= 10 + level;
                    level++;
                }
                return level + score / (10 + level);
            })
            { }
        }
    }
}
