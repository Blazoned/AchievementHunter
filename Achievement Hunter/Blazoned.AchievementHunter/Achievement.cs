using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter
{
    public class Achievement
    {
        #region Fields
        private bool _isCompleted;

        /// <summary>
        /// Gets the achievement ID.
        /// </summary>
        public string ID { get; private set; }
        /// <summary>
        /// Gets the title of the achievement.
        /// </summary>
        public string Title { get; private set; }
        /// <summary>
        /// Gets the description of the achievement.
        /// </summary>
        public string Description { get; private set; }
        /// <summary>
        /// Gets the score this achievement grants a user.
        /// </summary>
        public int Score { get; private set; }
        /// <summary>
        /// Gets the target count of the achievement.
        /// </summary>
        public int Goal { get; private set; }
        /// <summary>
        /// Gets the progression of the achievement.
        /// </summary>
        public int Counter { get; private set; }
        /// <summary>
        /// Gets the achievement type.
        /// </summary>
        public EAchievementType AchievementType { get; private set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiate an achievement object.
        /// </summary>
        /// <param name="title">The achievement title.</param>
        /// <param name="description">The description of the achievement.</param>
        /// <param name="score">The score granted by the achievement.</param>
        /// <param name="goal">The goal the achievement counter has to reach to be achieved.</param>
        public Achievement(string title, string description, int score, int goal = -1)
        {
            this.Title = title;
            this.Description = description;
            this.Score = score;

            if (goal < 1)
            {
                this.AchievementType = EAchievementType.Trigger;
            }
            else
            {
                this.Goal = goal;
                this.AchievementType = EAchievementType.Score;
            }

            this.Counter = 0;
            this._isCompleted = false;

            // TODO: Add a unique identification based on the achievement details.
            this.ID = "SOMETHING UNIQUELY GENERATED.";
        }
        #endregion

        #region Functions
        /// <summary>
        /// Increases the goal counter of the achievement. This function also accepts negative increments.
        /// </summary>
        /// <param name="increment">The amount to add to the goal counter.</param>
        /// <returns>Returns true if the achievement has been cokmpleted because of the counter increment, else returns false.</returns>
        public bool IncreaseCounter(int increment)
        {
            if (!_isCompleted)
            {
                Counter += increment;

                if (Counter > Goal)
                    Counter = Goal;
                if (Counter < 0)
                    Counter = 0;

                return IsCompleted();
            }

            return false;
        }
        /// <summary>
        /// Gets if the achievement has been completed.
        /// </summary>
        /// <returns>Returns true if the achievement has been completed.</returns>
        public bool IsCompleted()
        {
            if (!_isCompleted)
            {
                _isCompleted = Counter == Goal;
            }
            return _isCompleted;
        }
        #endregion

        #region Methods
        
        #endregion
    }
}
