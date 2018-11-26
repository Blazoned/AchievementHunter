using Blazoned.AchievementHunter.Entities;
using System.Collections.Generic;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IAchievementDAL : IConnectable
    {
        #region Functions
        #region Get Data
        /// <summary>
        /// Check if the database is already populated with achievements the given achievements.
        /// </summary>
        /// <param name="achievements">The achievements to check for.</param>
        /// <returns>Returns false if there are specified achievements missing in the database.</returns>
        bool IsPopulated(IEnumerable<AchievementEnt> achievements);
        #endregion

        #region Create Data
        /// <summary>
        /// Create an achievement in the database.
        /// </summary>
        /// <param name="achievement">The achievement to add to the database.</param>
        /// <returns>Returns true if the achievement has been added.</returns>
        bool CreateAchievement(AchievementEnt achievement);
        /// <summary>
        /// Populates the database with the given achievements.
        /// </summary>
        /// <param name="achievements">The list of achievements to populate the database with.</param>
        /// <param name="overwrite">Set to true if to overwrite the existing achievements.</param>
        /// <returns>Returns false if the database has not been updated.</returns>
        bool PopulateDatabase(IEnumerable<AchievementEnt> achievements, bool overwrite = true);
        #endregion

        #region Delete Data
        /// <summary>
        /// Delete an achievement in the database.
        /// </summary>
        /// <param name="achievement">The achievement to remove from the database.</param>
        /// <returns>Returns false if the achievement couldn't be removed.</returns>
        bool DeleteAchievement(string achievementId);
        /// <summary>
        /// Delete all achievements saved in the database.
        /// </summary>
        /// <returns>Returns false if the database has not made any changes.</returns>
        bool DeleteAchievements();
        #endregion
        #endregion
    }
}
