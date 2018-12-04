using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Structs;
using System.Collections.Generic;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Configuration
{
    public interface IConfigurationDAL
    {
        #region Functions
        #region Get Data
        /// <summary>
        /// Get the connection and its database type.
        /// </summary>
        /// <returns>Returns the connection string.</returns>
        string GetConnection();
        /// <summary>
        /// Get the database configuration.
        /// </summary>
        /// <returns>Returns the configuration for the database.</returns>
        DatabaseInfoDataStruct GetDatabaseConfiguration();
        /// <summary>
        /// Get the achievement configuration for a database.
        /// </summary>
        /// <returns>Returns the achievements to push to the database.</returns>
        IEnumerable<AchievementEnt> GetAchievementDatabaseConfiguration();
        #endregion

        #region Create Data
        /// <summary>
        /// Add an achievement to the configuration file.
        /// </summary>
        /// <param name="achievement">The achievement data to add to the configuration file.</param>
        void AddAchievement(AchievementEnt achievement);
        #endregion

        #region Delete Data
        /// <summary>
        /// Remove an achievement from the configuration file.
        /// </summary>
        /// <param name="achievementId">The id of the achievement which to remove from the configuration file.</param>
        void RemoveAchievement(string achievementId);
        #endregion
        #endregion
    }
}
