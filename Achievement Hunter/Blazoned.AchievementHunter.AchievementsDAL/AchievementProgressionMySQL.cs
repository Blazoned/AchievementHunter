using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Interfaces.Configuration;
using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.MySQL
{
    public class AchievementProgressionMySQL : IAchievementProgressionDAL
    {
        #region Fields
        /// <summary>
        /// The connection to use.
        /// </summary>
        private IConnectable _connectable;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate the achievement progression data access object.
        /// </summary>
        /// <param name="connectable">The connection object.</param>
        public AchievementProgressionMySQL(IConnectable connectable)
        {
            this._connectable = connectable;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Get a user's achievement progress.
        /// </summary>
        /// <param name="userId">The id of user from whom to retrieve their progression.</param>
        /// <returns>Returns a list with the user's achievement progression.</returns>
        public IEnumerable<UserAchievementEnt> GetAchievementProgression(string userId)
        {
            // TODO: Get the user's achievement progression from the database.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Update a user's achievement progression.
        /// </summary>
        /// <param name="progression">The user's achievement progress.</param>
        public void UpdateAchievementProgression(UserAchievementEnt progression)
        {
            // TODO: Update the user's achievement progression from the database.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete a user's achievement progress.
        /// </summary>
        /// <param name="userId">The user id of the user of whom to delete their achievement data.</param>
        public void DeleteUserData(string userId)
        {
            // TODO: Delete the user's achievement progression from the database.
            throw new NotImplementedException();
        }
        #endregion
    }
}
