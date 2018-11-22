using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IAchievementProgressionDAL : IConnectable
    {
        #region Functions
        #region Get Data
        /// <summary>
        /// Get a user's achievement progress.
        /// </summary>
        /// <param name="userId">The id of user from whom to retrieve their progression.</param>
        /// <returns>Returns the user's achievement progression.</returns>
        IEnumerable<AchievementProgressionStruct> GetAchievementProgression(string userId);
        /// <summary>
        /// Get a list of users' achievement progressions.
        /// </summary>
        /// <param name="userIds">The ids of the users from whom to retieve their achievement progression.</param>
        /// <returns>Returns a list of the users' achievement progression.</returns>
        IEnumerable<AchievementProgressionStruct>[] GetAchievementProgressions(string[] userIds);
        #endregion

        #region Update Data
        /// <summary>
        /// Update a user's achievement progression.
        /// </summary>
        /// <param name="progression">The user's achievement progress.</param>
        /// <returns>Returns false if there haven't been any changes in the database.</returns>
        bool UpdateAchievementProgression(AchievementProgressionStruct progression);
        #endregion

        #region Delete Data
        /// <summary>
        /// Delete a user's achievement progress.
        /// </summary>
        /// <param name="userId">The user id of the user of whom to delete their achievement data.</param>
        /// <returns>Returns false if the database has not made any changes.</returns>
        bool DeleteUserData(string userId);
        #endregion
        #endregion
    }
}
