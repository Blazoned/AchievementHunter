using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Structs
{
    public struct AchievementStruct
    {
        #region Fields
        /// <summary>
        /// The achievement identifier.
        /// </summary>
        public readonly string id;
        /// <summary>
        /// The achievement title.
        /// </summary>
        public readonly string title;
        /// <summary>
        /// The description or flavour text of the achievement.
        /// </summary>
        public readonly string description;
        /// <summary>
        /// The score granted by the achievement.
        /// </summary>
        public readonly int score;
        /// <summary>
        /// The goal the achievement counter has to reach to be achieved. If it's set to less than 1, the achievement will be treated as triggerable.
        /// </summary>
        public readonly int goal;
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiate an achievement struct.
        /// </summary>
        /// <param name="id">The achievement identifier.</param>
        /// <param name="title">The achievement title.</param>
        /// <param name="description">The description or flavour text of the achievement.</param>
        /// <param name="score">The score granted by the achievement.</param>
        /// <param name="goal">The goal the achievement counter has to reach to be achieved. If it's set to less than 1, the achievement will be treated as triggerable.</param>
        public AchievementStruct(string id, string title, string description, int score, int goal = -1)
        {
            this.id = id;
            this.title = title;
            this.description = description;
            this.score = score;
            this.goal = goal;
        }
        #endregion
    }
}
