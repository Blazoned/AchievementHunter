using Blazoned.AchievementHunter.IDAL.Structs;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IDBPrepDAL
    {
        #region Functions
        #region Get Data
        /// <summary>
        /// Check if the achievement database extention has been correctly prepared and build.
        /// </summary>
        /// <returns>Returns true if the database has already been populated.</returns>
        bool IsDatabaseCreated();
        #endregion

        #region Create Data
        /// <summary>
        /// Prepare and build the achievement database extention.
        /// </summary>
        void PrepareDatabase();
        #endregion
        #endregion
    }
}
