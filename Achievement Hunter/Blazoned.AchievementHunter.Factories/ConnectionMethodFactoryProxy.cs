using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Blazoned.AchievementHunter.IDAL.Interfaces.Configuration;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Structs;
using Blazoned.AchievementHunter.DAL.Configuration;

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

            ConnectionStruct connection = _configurationDAL.GetConnection();
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
        /// <returns>Returns false if the database has not been changed.</returns>
        public bool AddAchievement(AchievementStruct achievement, bool updateConfiguration = false)
        {
            using (var achievementsDAL = GetAchievementDataAccess())
            {
                if (!achievementsDAL.CreateAchievement(achievement))
                    return false;
            }

            if (updateConfiguration)
                _configurationDAL.AddAchievement(achievement);

            return true;
        }
        /// <summary>
        /// Removes an achievement from the database and updates the configuration file.
        /// </summary>
        /// <param name="achievementId">The id of the achievement which to delete.</param>
        /// <param name="updateConfiguration">Set to true if the configuration file has to be overwritten.</param>
        /// <returns>Returns false if the database has not been changed.</returns>
        public bool RemoveAchievement(string achievementId, bool updateConfiguration = false)
        {
            using (var achievementsDAL = GetAchievementDataAccess())
            {
                if(achievementsDAL.DeleteAchievement(achievementId))
                    return false;
            }

            if (updateConfiguration)
                _configurationDAL.RemoveAchievement(achievementId);

            return true;
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
        public IEnumerable<AchievementProgressionStruct> GetUserAchievements(string userId)
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
        /// <returns>Returns false if the database has not been changed.</returns>
        public bool UpdateUserProgression(AchievementProgressionStruct achievementProgression)
        {
            using (var achievementProgressionDAL = GetAchievementProgressionDataAccess())
            {
                return achievementProgressionDAL.UpdateAchievementProgression(achievementProgression);
            }
        }
        /// <summary>
        /// Permanently delete a user's achievement data.
        /// </summary>
        /// <param name="userId">The user from whom to delete their achievement records.</param>
        /// <returns>Returns false if the database has not been changed.</returns>
        public bool DeleteUserData(string userId)
        {
            using (var achievementProgressionDAL = GetAchievementProgressionDataAccess())
            {
                return achievementProgressionDAL.DeleteUserData(userId);
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
        /// <returns>Returns false if the database is already correctly prepared.</returns>
        private bool PrepareDatabase(DatabaseInfoStruct databaseInfo)
        {
            IDBPrepDAL dbPrepDal = GetDBPreparationDataAccess();
            bool isPrepared = dbPrepDal.IsDatabaseCreated(databaseInfo);

            if (!isPrepared)
                return isPrepared;

            return dbPrepDal.PrepareDatabase(databaseInfo);
        }

        /// <summary>
        /// Populates the database with achievements.
        /// </summary>
        /// <param name="achievements">The achievements to populate the database with.</param>
        /// <param name="overwrite">Whether or not to reset the database.</param>
        /// <returns>Returns false if the database population has not been changed.</returns>
        private bool PopulateDatabase(IEnumerable<AchievementStruct> achievements, bool overwrite = false)
        {
            IAchievementDAL achievementDal = GetAchievementDataAccess();
            
            bool isPopulated = achievementDal.IsPopulated(achievements);

            if (isPopulated && !overwrite)
                return false;

            if (overwrite)
                achievementDal.DeleteAchievements();

            foreach(AchievementStruct achievement in achievements)
            {
                achievementDal.CreateAchievement(achievement);
            }

            return true;
        }
        #endregion
        #endregion
    }
}
