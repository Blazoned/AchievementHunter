using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Structs
{
    public struct AchievementProgressionStruct
    {
        #region Fields
        /// <summary>
        /// The user identifier of the achievement progress.
        /// </summary>
        public readonly string userId;
        /// <summary>
        /// The achievement about which the progression is.
        /// </summary>
        public readonly AchievementStruct achievement;
        /// <summary>
        /// The current progress of the achievement.
        /// </summary>
        public readonly int counter;
        /// <summary>
        /// Whether or not the achievement has been completed.
        /// </summary>
        public readonly bool isCompleted;
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiate an achievement progression struct.
        /// </summary>
        /// <param name="userId">The user identifier of the achievement progress.</param>
        /// <param name="achievement">The achievement about which the progression is.</param>
        /// <param name="counter">The current progress of the achievement.</param>
        /// <param name="isCompleted">Whether or not the achievement has been completed.</param>
        public AchievementProgressionStruct(string userId, AchievementStruct achievement, int counter = 0, bool isCompleted = false)
        {
            this.userId = userId;
            this.achievement = achievement;
            this.counter = counter;
            this.isCompleted = isCompleted;
        }
        #endregion
    }
}
