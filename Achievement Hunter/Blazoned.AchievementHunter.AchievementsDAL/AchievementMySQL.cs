using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Interfaces.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.MySQL
{
    public class AchievementMySQL : IAchievementDAL
    {
        #region Fields
        /// <summary>
        /// The connection to use.
        /// </summary>
        private IConnectable _connectable;
        /// <summary>
        /// The configuration data access to use.
        /// </summary>
        private IConfigurationDAL _configurationDAL;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate the achievement data access object.
        /// </summary>
        /// <param name="connectable">The connection object.</param>
        /// <param name="configurationDAL">The configuration data access.</param>
        public AchievementMySQL(IConnectable connectable, IConfigurationDAL configurationDAL)
        {
            this._connectable = connectable;
            this._configurationDAL = configurationDAL;
        }
        #endregion

        #region Functions
        #region Database Preparation
        /// <summary>
        /// Check if the database is already populated with the specified achievements.
        /// </summary>
        /// <returns>Returns true if the database has been populated with achievements.</returns>
        public bool IsPopulated()
        {
            return IsDatabasePopulated(_configurationDAL.GetAchievementDatabaseConfiguration());
        }

        /// <summary>
        /// Populates the database with the given achievements.
        /// </summary>
        /// <param name="overwrite">Set to true if to overwrite the existing achievements.</param>
        /// <returns>Returns false if the database has not been updated.</returns>
        public void PopulateDatabase(bool overwrite = false)
        {
            IEnumerable<AchievementEnt> achievements = _configurationDAL.GetAchievementDatabaseConfiguration();

            if (overwrite || !IsDatabasePopulated(achievements))
            {
                Populate(achievements);
            }
        }
        #endregion

        #region Data Access
        /// <summary>
        /// Create an achievement in the database.
        /// </summary>
        /// <param name="achievement">The achievement to add to the database.</param>
        /// <param name="updateConfiguration">Set to true if the configuration file has to be overwritten.</param>
        public void CreateAchievement(AchievementEnt achievement, bool updateConfig = false)
        {
            if (updateConfig)
                _configurationDAL.AddAchievement(achievement);

            // TODO: Save achievement in the database.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Delete an achievement in the database.
        /// </summary>
        /// <param name="achievement">The achievement to remove from the database.</param>
        /// <param name="updateConfiguration">Set to true if the configuration file has to be overwritten.</param>
        public void DeleteAchievement(string achievementId, bool updateConfig = false)
        {
            if (updateConfig)
                _configurationDAL.RemoveAchievement(achievementId);

            // TODO: Delete achievement in the database.
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reset all achievements saved in the database.
        /// </summary>
        public void ResetAchievements()
        {
            // TODO: Reset the database.
            throw new NotImplementedException();
        }
        #endregion
        #endregion

        #region Methods
        /// <summary>
        /// Check if the database has been populated or not.
        /// </summary>
        /// <param name="achievements">The achievements which to look for in the databbase.</param>
        /// <returns>Returns true if the database has already been populated with achievements.</returns>
        private bool IsDatabasePopulated(IEnumerable<AchievementEnt> achievements)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Prepare the database.
        /// </summary>
        /// <param name="achievements">The achievements with which to populate the databbase.</param>
        private void Populate(IEnumerable<AchievementEnt> achievements)
        {
            foreach (AchievementEnt achievement in achievements)
            {
                CreateAchievement(achievement);
            }
        }
        #endregion
    }
}
