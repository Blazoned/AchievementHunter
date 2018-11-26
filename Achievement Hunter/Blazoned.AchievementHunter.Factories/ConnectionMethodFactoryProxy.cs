using Blazoned.AchievementHunter.DAL.Configuration;
using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Interfaces.Configuration;
using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Blazoned.AchievementHunter.Factories
{
    // TODO: Turn proxy into singleton and remove object pool
    public class ConnectionMethodFactoryProxy
    {
        #region Fields
        /// <summary>
        /// The global instance of the method factory proxy.
        /// </summary>
        private static ConnectionMethodFactoryProxy _instance;

        /// <summary>
        /// The configuration data access.
        /// </summary>
        private IConfigurationDAL _configurationDAL;
        /// <summary>
        /// The actual method factory being invoked.
        /// </summary>
        private IConnectionMethodFactory _connectionMethodFactory;
        #endregion

        #region Constructors
        private ConnectionMethodFactoryProxy()
        {
            _configurationDAL = new ConfigurationDAL();

            ConnectionDataStruct connection = _configurationDAL.GetConnection();
            this._connectionMethodFactory = LoadMethodFactory(connection.databaseType, connection.connectionString);

            PrepareDatabase(_configurationDAL.GetDatabaseConfiguration());
            PopulateDatabase(_configurationDAL.GetAchievementDatabaseConfiguration());
        }
        #endregion

        #region Functions
        #region Instantiation
        public static ConnectionMethodFactoryProxy GetInstance()
        {
            if (_instance == null)
                _instance = new ConnectionMethodFactoryProxy();

            return _instance;
        }
        #endregion

        #region Proxy Functions
        #region Achievements
        /// <summary>
        /// Adds an achievement to the database and updates the configuration file.
        /// </summary>
        /// <param name="achievement">The achievement to add to the database.</param>
        /// <param name="updateConfiguration">Set to true if the configuration file has to be overwritten.</param>
        public void AddAchievement(AchievementEnt achievement, bool updateConfiguration = false)
        {
            using (var achievementsDAL = GetAchievementDataAccess())
            {
                if (!achievementsDAL.CreateAchievement(achievement))
                    updateConfiguration = false;
            }

            if (updateConfiguration)
                _configurationDAL.AddAchievement(achievement);
        }
        /// <summary>
        /// Removes an achievement from the database and updates the configuration file.
        /// </summary>
        /// <param name="achievementId">The id of the achievement which to delete.</param>
        /// <param name="updateConfiguration">Set to true if the configuration file has to be overwritten.</param>
        public void RemoveAchievement(string achievementId, bool updateConfiguration = false)
        {
            using (var achievementsDAL = GetAchievementDataAccess())
            {
                if(achievementsDAL.DeleteAchievement(achievementId))
                    updateConfiguration = false;
            }

            if (updateConfiguration)
                _configurationDAL.RemoveAchievement(achievementId);
        }
        /// <summary>
        /// Reloads the database using the achievement configuration data.
        /// </summary>
        public void ResetDatabase()
        {
            using (var achievementsDAL = GetAchievementDataAccess())
            {
                achievementsDAL.DeleteAchievements();

                var achievementsConfig = _configurationDAL.GetAchievementDatabaseConfiguration();

                achievementsDAL.PopulateDatabase(achievementsConfig);
            }
        }
        #endregion

        #region Achievement Progress
        /// <summary>
        /// Retrieve a user's achievement data.
        /// </summary>
        /// <param name="userId">The user from whom to retrieve their data.</param>
        /// <returns>Returns the user's achievement data.</returns>
        public IEnumerable<UserAchievementEnt> GetUserAchievements(string userId)
        {
            using (var achievementProgressionDAL = GetAchievementProgressionDataAccess())
            {
                return achievementProgressionDAL.GetAchievementProgression(userId);
            }
        }
        /// <summary>
        /// Update a user's achievement progress.
        /// </summary>
        /// <param name="achievementProgression">The achievement progression data.</param>
        public void UpdateUserProgression(UserAchievementEnt achievementProgression)
        {
            using (var achievementProgressionDAL = GetAchievementProgressionDataAccess())
            {
                achievementProgressionDAL.UpdateAchievementProgression(achievementProgression);
            }
        }
        /// <summary>
        /// Permanently delete a user's achievement data.
        /// </summary>
        /// <param name="userId">The user from whom to delete their achievement records.</param>
        public void DeleteUserData(string userId)
        {
            using (var achievementProgressionDAL = GetAchievementProgressionDataAccess())
            {
                achievementProgressionDAL.DeleteUserData(userId);
            }
        }
        #endregion
        #endregion
        #endregion

        #region Methods
        #region DAL
        /// <summary>
        /// Retrieves an instance of the database preparation data access.
        /// </summary>
        /// <returns>Returns an database preparation data access object.</returns>
        private IDBPrepDAL GetDBPreparationDataAccess()
        {
            return _connectionMethodFactory.GetDBPreparationDataAccess();
        }

        /// <summary>
        /// Retrieves an instance of the achievement data access.
        /// </summary>
        /// <returns>Returns an achievement data access object.</returns>
        private IAchievementDAL GetAchievementDataAccess()
        {
            return _connectionMethodFactory.GetAchievementDataAccess();
        }

        /// <summary>
        /// Retrieves an instance of the achievement progression data access.
        /// </summary>
        /// <returns>Returns an achievement progression data access object.</returns>
        private IAchievementProgressionDAL GetAchievementProgressionDataAccess()
        {
            return _connectionMethodFactory.GetAchievementProgressionDataAccess();
        }
        #endregion

        #region Initialisation
        /// <summary>
        /// Get the proper database method factory to use in this proxy.
        /// </summary>
        /// <param name="databaseType">The type of database to check the method factory against.</param>
        /// <param name="connection">The connection string to connect to the database.</param>
        /// <returns>Returns a usable method factory for the proxy to use.</returns>
        private IConnectionMethodFactory LoadMethodFactory(string databaseType, string connection)
        {
            Type factoryType = this.GetType().Assembly.GetTypes().Where
                (
                    type => type.IsClass &&
                    type.IsAssignableFrom(typeof(IConnectionMethodFactory)) &&
                    type.GetCustomAttribute<DatabaseAttribute>().DatabaseType == databaseType
                ).First();

            return (IConnectionMethodFactory)Activator.CreateInstance(factoryType, args: new object[] { connection });
        }

        /// <summary>
        /// Prepares the database for use.
        /// </summary>
        /// <param name="databaseInfo">The database configuration information.</param>
        private void PrepareDatabase(DatabaseInfoDataStruct databaseInfo)
        {
            using (IDBPrepDAL dbPrepDal = GetDBPreparationDataAccess())
            {
                dbPrepDal.PrepareDatabase(databaseInfo);
            }
        }

        /// <summary>
        /// Populates the database with achievements.
        /// </summary>
        /// <param name="achievements">The achievements to populate the database with.</param>
        /// <param name="overwrite">Whether or not to reset the database.</param>
        private void PopulateDatabase(IEnumerable<AchievementEnt> achievements, bool overwrite = true)
        {
            using (IAchievementDAL achievementDal = GetAchievementDataAccess())
            {
                achievementDal.PopulateDatabase(achievements, overwrite);
            }
        }
        #endregion
        #endregion
    }
}
