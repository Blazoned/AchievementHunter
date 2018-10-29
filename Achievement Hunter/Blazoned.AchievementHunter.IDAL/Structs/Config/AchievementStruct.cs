using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Structs.Config
{
    public struct AchievementStruct
    {
        #region Fields
        public readonly string id;
        public readonly string title;
        public readonly string description;
        public readonly int score;
        public readonly int goal;
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiate an achievement struct.
        /// </summary>
        /// <param name="id">The achievement identifier.</param>
        /// <param name="title">The title of the achievement.</param>
        /// <param name="description">The description or flavour text of the achievement.</param>
        /// <param name="score">The score that is granted to the user upon completion.</param>
        /// <param name="goal">The goal the counter has to </param>
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
