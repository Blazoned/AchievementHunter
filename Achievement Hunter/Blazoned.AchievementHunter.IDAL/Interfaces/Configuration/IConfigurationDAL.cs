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
    }
}
