using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Blazoned.AchievementHunter
{
    public class AchievementManager : IDisposable
    {
        #region Fields
        #region Dependencies
        private IAchievementDAL _achievementDAL;
        private IAchievementProgressionDAL _userAchievementDAL;
        #endregion

        #region Indexers
        /// <summary>
        /// Get an achievement from the achievement manager by its specified id.
        /// </summary>
        /// <param name="userID">The user id of the user from which to access the achievements.</param>
        /// <param name="achievementID">The identifier of the achievement.</param>
        /// <returns>Returns the achievement value corresponding to the identifier. Returns null if no such achievement exists.</returns>
        public UserAchievement this[string userID, string achievementID]
        {
            get { return (UserAchievement)_achievementListings[userID].Where(achievement => achievement.Id == achievementID); }
        }
        #endregion

        /// <summary>
        /// The list of achievements, on a per user base, maintained with this manager.
        /// </summary>
        /// The string value represents the user identifier, whereas the Hashtable contains the achievements for the specified user.
        private Dictionary<string, SortedSet<UserAchievement>> _achievementListings;
        #endregion

        #region Constructor
        public AchievementManager(IAchievementDAL achievementDAL, IAchievementProgressionDAL userAchievementDAL)
        {
            this._achievementDAL = achievementDAL;
            this._userAchievementDAL = userAchievementDAL;

            _achievementListings = new Dictionary<string, SortedSet<UserAchievement>>();
        }
        #endregion

        #region Functions
        #region Achievements
        #region Create
        /// <summary>
        /// Add a new achievement to the achievement manager. This achievement will also be added to the configuration file and the achievement data.
        /// </summary>
        /// <param name="id">The achievement identifier.</param>
        /// <param name="title">The achievement title.</param>
        /// <param name="description">The description or flavour text of the achievement.</param>
        /// <param name="score">The score granted by the achievement.</param>
        /// <param name="goal">The goal the achievement counter has to reach to be achieved. If it's set to less than 1, the achievement will be treated as triggerable.</param>
        /// <param name="updateConfiguration">Set to true to also add the achievement from the configuration settings.</param>
        public void AddAchievement(string id, string title, string description, int score, int goal = -1, bool updateConfiguration = false)
        {
            AddAchievement(new AchievementEnt(id, title, description, score, goal), updateConfiguration);
        }
        /// <summary>
        /// Add a new achievement to the achievement manager. This achievement will also be added to the configuration file and the achievement data.
        /// </summary>
        /// <param name="achievement">The achievement to add to the achievement system.</param>
        /// <param name="updateConfiguration">Set to true to also add the achievement from the configuration settings.</param>
        public void AddAchievement(AchievementEnt achievement, bool updateConfiguration = false)
        {
            _achievementDAL.CreateAchievement(achievement, updateConfiguration);

            foreach (var achievementsListing in _achievementListings)
            {
                achievementsListing.Value.Add(new UserAchievement(_userAchievementDAL, achievementsListing.Key, achievement));
            }
        }
        #endregion

        #region Update
        /// <summary>
        /// Reload the achievements from the configuration file. Existing users in the achievement manager will be updated.
        /// </summary>
        public void ResetAchievements()
        {
            List<string> loadedUserIds = _achievementListings.Keys.ToList();

            _achievementListings.Clear();
            _achievementDAL.ResetAchievements();

            foreach (var userId in loadedUserIds)
            {
                LoadUserProgress(userId);
            }
        }
        #endregion

        #region Delete
        /// <summary>
        /// Remove an achievement from the data records.
        /// </summary>
        /// <param name="achievementId">The achievement which to remove.</param>
        /// <param name="updateConfiguration">Set to true to also delete the achievement from the configuration settings.</param>
        public void DeleteAchievement(string achievementId, bool updateConfiguration = false)
        {
            _achievementDAL.DeleteAchievement(achievementId, updateConfiguration);

            foreach(var userAchievements in _achievementListings)
            {
                userAchievements.Value.Remove(
                    userAchievements.Value.Where(achievement => achievement.Id == achievementId).FirstOrDefault());
            }
        }
        #endregion
        #endregion

        #region UserAchievements
        #region Get
        /// <summary>
        /// Load the specified user into the achievement manager.
        /// </summary>
        /// <param name="userId">The identifier of the user for whom to retrieve their achievements.</param>
        public void LoadUserProgress(string userId)
        {
            List<UserAchievementEnt> achievements = new List<UserAchievementEnt>(
               _userAchievementDAL.GetAchievementProgression(userId));

            SortedSet<UserAchievement> achievementProgress = new SortedSet<UserAchievement>();

            foreach (var achievement in achievements)
            {
                achievementProgress.Add(new UserAchievement(_userAchievementDAL, achievement));
            }

            _achievementListings.Add(userId, achievementProgress);
        }
        #endregion

        #region Delete
        /// <summary>
        /// Clear a user's achievement data from the achievement manager.
        /// </summary>
        /// <param name="userId">The user whom to clear from memory.</param>
        public void ClearUserData(string userId)
        {
            _achievementListings.Remove(userId);
        }
        /// <summary>
        /// Permanently remove a user's achievement data records.
        /// </summary>
        /// <param name="userId">The user whom to remove from the records.</param>
        public void DeleteUserData(string userId)
        {
            _userAchievementDAL.DeleteUserData(userId);
            ClearUserData(userId);
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
            _achievementListings.Clear();
        }
        #endregion
        #endregion

        #region Methods

        #endregion
    }
}
