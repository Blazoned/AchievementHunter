using Blazoned.AchievementHunter.Entities;
using System.Collections.Generic;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IAchievementProgressionDAL
    {
        #region Functions
        #region Get Data
        /// <summary>
        /// Get a user's achievement progress.
        /// </summary>
        /// <param name="userId">The id of user from whom to retrieve their progression.</param>
        /// <returns>Returns a list with the user's achievement progression.</returns>
        IEnumerable<UserAchievementEnt> GetAchievementProgression(string userId);
        #endregion

        #region Update Data
        /// <summary>
        /// Update a user's achievement progression.
        /// </summary>
        /// <param name="progression">The user's achievement progress.</param>
        void UpdateAchievementProgression(UserAchievementEnt progression);
        #endregion

        #region Delete Data
        /// <summary>
        /// Delete a user's achievement progress.
        /// </summary>
        /// <param name="userId">The user id of the user of whom to delete their achievement data.</param>
        void DeleteUserData(string userId);
        #endregion
        #endregion
    }
}
