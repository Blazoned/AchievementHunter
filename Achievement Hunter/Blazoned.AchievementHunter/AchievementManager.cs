using Blazoned.AchievementHunter.Extentions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter
{
    public class AchievementManager : IDisposable
    {
        #region Fields
        #region Indexers
        /// <summary>
        /// Get an achievement from the achievement manager by its specified id.
        /// </summary>
        /// <param name="userID">The user id of the user from which to access the achievements.</param>
        /// <param name="achievementID">The identifier of the achievement.</param>
        /// <returns>Returns the achievement value corresponding to the identifier. Returns null if no such achievement exists.</returns>
        public Achievement this[string userID, string achievementID]
        {
            get { return (Achievement)_achievements[userID][achievementID]; }
        }
        #endregion

        /// <summary>
        /// The singleton instance of this class.
        /// </summary>
        private static AchievementManager _this;
        /// <summary>
        /// The list of achievements, on a per user base, maintained with this manager.
        /// </summary>
        /// The string value represents the user identifier, whereas the Hashtable contains the achievements for the specified user.
        private Dictionary<string, Hashtable> _achievements;
        #endregion

        #region Constructor
        private AchievementManager()
        {
            PopulateAchievements();
        }
        #endregion

        #region Functions
        #region Instantiation
        /// <summary>
        /// Get the instance of the achievement manager.
        /// </summary>
        /// <returns>Returns an instance of the achievement manager.</returns>
        public AchievementManager GetInstance()
        {
            if (_this == null)
            {
                _this = new AchievementManager();
            }
            return _this;
        }
        #endregion

        #region Achievements
        #region Add
        /// <summary>
        /// Add a new achievement to the achievement manager. This achievement will also be added to the configuration file and the achievement data.
        /// </summary>
        /// <param name="id">The achievement identifier.</param>
        /// <param name="title">The achievement title.</param>
        /// <param name="description">The description or flavour text of the achievement.</param>
        /// <param name="score">The score granted by the achievement.</param>
        /// <param name="goal">The goal the achievement counter has to reach to be achieved. If it's set to less than 1, the achievement will be treated as triggerable.</param>
        /// <returns>Returns false if the achievement already exists. Else returns true.</returns>
        public bool AddAchievement(string id, string title, string description, int score, int goal = -1)
        {
            return AddAchievement(new Achievement(id, title, description, score, goal));
        }
        /// <summary>
        /// Add a new achievement to the achievement manager. This achievement will also be added to the configuration file and the achievement data.
        /// </summary>
        /// <param name="achievement">The achievement to add to the achievement system.</param>
        /// <returns>Returns false if the achievement already exists. Else returns true.</returns>
        public bool AddAchievement(Achievement achievement)
        {
            // TODO: Add a new achievement to the database and check if it already exists or not.
            bool success = false;

            // TODO: Don't add the achievement if it already exists.
            if (!success)
                return false;

            // Add the achievements to each user
            List<string> keys = new List<string>(_achievements.Keys);

            foreach (var key in keys)
            {
                _achievements[key].SafeAdd(achievement.ID, achievement);
            }

            return true;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="achievements"></param>
        public void AddAchievements(Dictionary<string, Achievement> achievements)
        {
            // TODO: Add range to database

            // Add the achievements to each user
            List<string> keys = new List<string>(_achievements.Keys);

            foreach(var key in keys)
            {
                _achievements[key].AddRange(new Hashtable(achievements));
            }
        }
        #endregion

        #region Load
        /// <summary>
        /// Load all the users into the achievement manager.
        /// </summary>
        public void LoadProgress()
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Load the specified user into the achievement manager.
        /// </summary>
        /// <param name="identifier">The user identifier of the user for which to retrieve their achievements.</param>
        public void LoadProgress(string identifier)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Reload the achievements from the configuration file. This also clears the achievement manager.
        /// </summary>
        public void ReloadAchievements()
        {
            _achievements.Clear();
            PopulateAchievements(true);
        }
        #endregion
        #endregion

        #region Dispose
        /// <summary>
        /// Disposes of the achievement manager.
        /// </summary>
        public void Dispose()
        {
            Clear();
        }
        /// <summary>
        /// Clear the achievement manager from any data.
        /// </summary>
        public void Clear()
        {
            _achievements.Clear();
        }
        #endregion
        #endregion

        #region Methods
        /// <summary>
        /// Populates the database with the achievements. The achievements have to be specified in the configuration file of the AchievementHunter DLL. By default the database will not be overwritten if they are already populated.
        /// </summary>
        private void PopulateAchievements(bool overwriteDatabase = false)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
