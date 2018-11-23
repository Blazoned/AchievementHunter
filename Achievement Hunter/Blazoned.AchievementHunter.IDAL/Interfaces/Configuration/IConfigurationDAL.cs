using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Configuration
{
    public interface IConfigurationDAL
    {
        #region Functions
        #region Get Data
        /// <summary>
        /// Get the connection and its database type.
        /// </summary>
        /// <returns>Returns struct with connection information.</returns>
        ConnectionStruct GetConnection();
        /// <summary>
        /// Get the database configuration.
        /// </summary>
        /// <returns>Returns the configuration for the database.</returns>
        DatabaseInfoStruct GetDatabaseConfiguration();
        /// <summary>
        /// Get the achievement configuration for a database.
        /// </summary>
        /// <returns>Returns the achievements to push to the database.</returns>
        IEnumerable<AchievementStruct> GetAchievementDatabaseConfiguration();
        #endregion

        #region Create Data
        /// <summary>
        /// Add an achievement to the configuration file.
        /// </summary>
        /// <param name="achievement">The achievement data to add to the configuration file.</param>
        void AddAchievement(AchievementStruct achievement);
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
