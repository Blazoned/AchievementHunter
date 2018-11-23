using Blazoned.AchievementHunter.IDAL.Structs;
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
        /// Gets the description or flavour text of the achievement.
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
        /// Gets whether or not the achievement has been completed.
        /// </summary>
        public bool IsCompleted {
            get
            {
                if (!_isCompleted && AchievementType != EAchievementType.Trigger)
                {
                    _isCompleted = Counter == Goal;
                }
                return _isCompleted;
            }
            private set
            {
                _isCompleted = value;
            }
        }
        /// <summary>
        /// Gets the achievement type.
        /// </summary>
        public EAchievementType AchievementType { get; private set; }
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate an achievement object.
        /// </summary>
        /// <param name="id">The achievement identifier.</param>
        /// <param name="title">The achievement title.</param>
        /// <param name="description">The description or flavour text of the achievement.</param>
        /// <param name="score">The score granted by the achievement.</param>
        /// <param name="goal">The goal the achievement counter has to reach to be achieved. If it's set to less than 1, the achievement will be treated as triggerable.</param>
        /// <param name="counter">The current progress of the achievement.</param>
        /// <param name="isCompleted">Whether or not the achievement has been completed.</param>
        public Achievement(string id, string title, string description, int score, int goal = -1, int counter = 0, bool isCompleted = false)
        {
            this.ID = id;
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

            this.Counter = counter;
            this.IsCompleted = isCompleted;
        }
        /// <summary>
        /// Instantiate an achievement object from struct data.
        /// </summary>
        /// <param name="achievementData"></param>
        public Achievement(AchievementProgressionStruct achievementData)
            : this(achievementData.achievement.id,
                   achievementData.achievement.title,
                   achievementData.achievement.description,
                   achievementData.achievement.score,
                   achievementData.achievement.goal,
                   achievementData.counter,
                   achievementData.isCompleted)
        {
            
        }
        #endregion

        #region Functions
        /// <summary>
        /// Triggers the achievement to be completed if the achievement is triggerable.
        /// </summary>
        /// <returns>Returns true if the achievement has been completed because of the trigger, else returns false.</returns>
        internal bool Trigger()
        {
            if (AchievementType == EAchievementType.Trigger && !_isCompleted)
            {
                return IsCompleted = true;
            }

            return false;
        }
        /// <summary>
        /// Sets the goal counter of the achievement.
        /// </summary>
        /// <param name="count">The score to which to set the goal counter.</param>
        /// <returns>Returns true if the achievement has been completed because of the set counter, else returns false.</returns>
        internal bool SetCounter(int count)
        {
            if (AchievementType == EAchievementType.Score && !_isCompleted)
            {
                Counter = count;

                LimitCounter();

                return IsCompleted;
            }
            return _isCompleted;
        }
        /// <summary>
        /// Increases the goal counter of the achievement. This function also accepts negative increments.
        /// </summary>
        /// <param name="increment">The amount to add to the goal counter.</param>
        /// <returns>Returns true if the achievement has been cokmpleted because of the counter increment, else returns false.</returns>
        internal bool IncreaseCounter(int increment = 1)
        {
            if (AchievementType == EAchievementType.Score && !_isCompleted)
            {
                Counter += increment;

                LimitCounter();

                return IsCompleted;
            }

            return false;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Change the value to 0 or the goal of the achievement if the goal counter is out of bounds.
        /// </summary>
        private void LimitCounter()
        {
            if (Counter > Goal)
                Counter = Goal;
            if (Counter < 0)
                Counter = 0;
        }
        #endregion
    }
}
