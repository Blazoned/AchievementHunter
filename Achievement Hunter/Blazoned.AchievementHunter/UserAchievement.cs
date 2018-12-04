using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;

namespace Blazoned.AchievementHunter
{
    public class UserAchievement
    {
        #region Fields
        #region Dependencies
        IAchievementProgressionDAL _achievementProgressionDAL;
        #endregion

        /// <summary>
        /// Retains whether or not the achievement has been completed.
        /// </summary>
        private bool _isCompleted;

        /// <summary>
        /// Gets the user id.
        /// </summary>
        public string UserId { get; private set; }
        /// <summary>
        /// Gets the achievement id.
        /// </summary>
        public string Id { get; private set; }
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
        /// <param name="userId">The user identifier whom to match this achievement with.</param>
        /// <param name="id">The achievement identifier.</param>
        /// <param name="title">The achievement title.</param>
        /// <param name="description">The description or flavour text of the achievement.</param>
        /// <param name="score">The score granted by the achievement.</param>
        /// <param name="goal">The goal the achievement counter has to reach to be achieved. If it's set to less than 1, the achievement will be treated as triggerable.</param>
        /// <param name="counter">The current progress of the achievement.</param>
        /// <param name="isCompleted">Whether or not the achievement has been completed.</param>
        private UserAchievement(IAchievementProgressionDAL achievementProgressionDAL, string userId, string id, string title, string description, int score, int goal, int counter = 0, bool isCompleted = false)
        {
            this._achievementProgressionDAL = achievementProgressionDAL;

            this.UserId = userId;
            this.Id = id;
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
        /// <param name="achievementData">The achievement data which to unpack.</param>
        internal UserAchievement(IAchievementProgressionDAL achievementProgressionDAL, UserAchievementEnt achievementData)
            : this(achievementProgressionDAL,
                   achievementData.userId,
                   achievementData.achievement.id,
                   achievementData.achievement.title,
                   achievementData.achievement.description,
                   achievementData.achievement.score,
                   achievementData.achievement.goal,
                   achievementData.counter,
                   achievementData.isCompleted)
        {
            
        }
        /// <summary>
        /// Instantiate an achievement object from empty struct data.
        /// </summary>
        /// <param name="userId">The user whom to match the data to.</param>
        /// <param name="achievement">The achievement data which to unpack.</param>
        internal UserAchievement(IAchievementProgressionDAL achievementProgressionDAL, string userId, AchievementEnt achievement)
            : this(achievementProgressionDAL,
                   userId,
                   achievement.id,
                   achievement.title,
                   achievement.description,
                   achievement.score,
                   achievement.goal)
        {
            this._achievementProgressionDAL.UpdateAchievementProgression(this);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Triggers the achievement to be completed if the achievement is triggerable.
        /// </summary>
        /// <returns>Returns true if the achievement has been completed because of the trigger, else returns false.</returns>
        public bool Trigger()
        {
            if (AchievementType == EAchievementType.Trigger && !_isCompleted)
            {
                IsCompleted = true;

                _achievementProgressionDAL.UpdateAchievementProgression(this);

                return IsCompleted;
            }

            return false;
        }
        /// <summary>
        /// Sets the goal counter of the achievement.
        /// </summary>
        /// <param name="count">The score to which to set the goal counter.</param>
        /// <returns>Returns true if the achievement has been completed because of the new counter value, else returns false.</returns>
        public bool SetCounter(int count)
        {
            if (AchievementType == EAchievementType.Score && !_isCompleted)
            {
                Counter = count;
                LimitCounter();

                bool returnVal = IsCompleted;

                _achievementProgressionDAL.UpdateAchievementProgression(this);

                return returnVal;
            }

            return _isCompleted;
        }
        /// <summary>
        /// Increases the goal counter of the achievement. This function also accepts negative increments.
        /// </summary>
        /// <param name="increment">The amount to add to the goal counter.</param>
        /// <returns>Returns true if the achievement has been completed because of the new counter value, else returns false.</returns>
        public bool IncreaseCounter(int increment = 1)
        {
            if (AchievementType == EAchievementType.Score && !_isCompleted)
            {
                Counter += increment;
                LimitCounter();

                bool returnVal = IsCompleted;

                _achievementProgressionDAL.UpdateAchievementProgression(this);

                return returnVal;
            }

            return false;
        }
        #endregion

        #region Conversion Operations
        /// <summary>
        /// Convert an achievement object to an achievement entity.
        /// </summary>
        /// <param name="achievement">The entity to convert.</param>
        public static implicit operator UserAchievementEnt(UserAchievement achievement)
        {
            return new UserAchievementEnt(
                   achievement.UserId,
                   new AchievementEnt(
                       achievement.Id,
                       achievement.Title,
                       achievement.Description,
                       achievement.Score,
                       achievement.Goal),
                   achievement.Counter,
                   achievement.IsCompleted);
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
