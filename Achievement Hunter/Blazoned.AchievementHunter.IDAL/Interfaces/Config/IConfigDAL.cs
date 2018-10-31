using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazoned.AchievementHunter.IDAL.Structs;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Config
{
    public interface IConfigDAL
    {
        /// <summary>
        /// Load the database connection settings found in the configuration files.
        /// </summary>
        /// <returns>Returns the connection settings.</returns>
        DatabaseInfoStruct LoadDataAccessConfiguration();
        /// <summary>
        /// Load the achievements from the configurations.
        /// </summary>
        /// <returns>Returns the achievements found in the configuration files.</returns>
        AchievementStruct[] LoadAchievements();
    }
}
