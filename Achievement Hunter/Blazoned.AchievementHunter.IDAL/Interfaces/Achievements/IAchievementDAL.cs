using Blazoned.AchievementHunter.Entities;
using System.Collections.Generic;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IAchievementDAL
    {
        #region Functions
        #region Get Data
        /// <summary>
        /// Check if the database is already populated with achievements the given achievements.
        /// </summary>
        /// <returns>Returns false if there are specified achievements missing in the database.</returns>
        bool IsPopulated();
        #endregion

        #region Create Data
        /// <summary>
        /// Create an achievement in the database.
        /// </summary>
        /// <param name="achievement">The achievement to add to the database.</param>
        /// <param name="updateConfiguration">Set to true if the configuration file has to be overwritten.</param>
        void CreateAchievement(AchievementEnt achievement, bool updateConfig = false);
        /// <summary>
        /// Populates the database with the given achievements.
        /// </summary>
        /// <param name="overwrite">Set to true if to overwrite the existing achievements.</param>
        void PopulateDatabase(bool overwrite = false);
        #endregion

        #region Delete Data
        /// <summary>
        /// Delete an achievement in the database.
        /// </summary>
        /// <param name="achievement">The achievement to remove from the database.</param>
        /// <param name="updateConfiguration">Set to true if the configuration file has to be overwritten.</param>
        void DeleteAchievement(string achievementId, bool updateConfig = false);
        /// <summary>
        /// Reset all achievements saved in the database.
        /// </summary>
        void ResetAchievements();
        #endregion
        #endregion
    }
}
