using Blazoned.AchievementHunter.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Blazoned.AchievementHunter.DAL.InMemory.Database
{
    internal class InMemoryDatabase
    {
        #region Fields
        private static InMemoryDatabase _instance;
        
        private Dictionary<string, List<UserAchievementEnt>> _userAchievements;
        private List<AchievementEnt> _achievements;
        #endregion

        #region Constructor
        private InMemoryDatabase()
        {
            this._userAchievements = new Dictionary<string, List<UserAchievementEnt>>();
            this._achievements = new List<AchievementEnt>();
        }
        #endregion

        #region Function
        #region Instantiation
        /// <summary>
        /// Get the InMemoryDatabase instance.
        /// </summary>
        /// <returns>Returns the InMemoryDatabase instance.</returns>
        public static InMemoryDatabase GetInstance()
        {
            if (_instance == null)
            {
                _instance = new InMemoryDatabase();
            }

            return _instance;
        }
        #endregion

        #region Database
        #region Achievements
        public bool IsPopulated(IEnumerable<AchievementEnt> achievements)
        {
            bool hasItems = true;

            foreach(AchievementEnt achievement in achievements)
            {
                hasItems &= _achievements.Where(achieve => achieve.id == achievement.id).Count() > 0;
            }

            return hasItems;
        }

        public bool CreateAchievement(AchievementEnt achievement)
        {
            _achievements.Add(achievement);

            return true;
        }

        public bool PopulateDatabase(IEnumerable<AchievementEnt> achievements, bool overwrite)
        {
            if (overwrite)
                _achievements = new List<AchievementEnt>(achievements);
            else
                _achievements.AddRange(achievements);

            return true;
        }

        public bool DeleteAchievement(string achievementId)
        {
            return _achievements.Remove(
                        _achievements.Where(achievement => achievement.id == achievementId).FirstOrDefault());
        }

        public bool DeleteAchievements()
        {
            _achievements.Clear();

            return true;
        }
        #endregion

        #region UserAchievements
        public IEnumerable<UserAchievementEnt> GetAchievementProgression(string userId)
        {
            if (!_userAchievements.ContainsKey(userId))
            {
                List<UserAchievementEnt> userAchievements = new List<UserAchievementEnt>();

                foreach (var achievement in _achievements)
                {
                    userAchievements.Add(new UserAchievementEnt(
                        userId,
                        achievement,
                        0,
                        false));
                }

                _userAchievements.Add(userId, userAchievements);
            }

            return _userAchievements[userId];
        }

        public bool UpdateAchievementProgression(UserAchievementEnt progression)
        {
            if (!_userAchievements.ContainsKey(progression.userId))
                return false;

            int index = _userAchievements[progression.userId].FindIndex(userAchievement => userAchievement.achievement.id == progression.achievement.id);
            _userAchievements[progression.userId][index] = progression;

            return true;
        }

        public bool DeleteUserData(string userId)
        {
            return _userAchievements.Remove(userId);
        }
        #endregion
        #endregion
        #endregion
    }
}
