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
        /// <param name="user">The user id of the user from which to access the achievements.</param>
        /// <param name="id">The identifier of the achievement.</param>
        /// <returns>Returns the achievement value corresponding to the identifier. Returns null if no such achievement exists.</returns>
        public Achievement this[string user, string id]
        {
            get { return (Achievement)_achievements[user][id]; }
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
