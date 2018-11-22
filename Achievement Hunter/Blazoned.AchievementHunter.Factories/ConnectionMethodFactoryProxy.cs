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
    // TODO: Combine proxy with pool manager
    public class ConnectionMethodFactoryProxy : IDisposable
    {
        #region Fields
        private IConnectionMethodFactory _connectionMethodFactory;
        #endregion

        #region Constructors
        internal ConnectionMethodFactoryProxy()
        {
            IConfigurationDAL configuration = new ConfigurationDAL();

            ConnectionStruct connection = configuration.GetConnection();
            this._connectionMethodFactory = LoadMethodFactory(connection.databaseType, connection.connectionString);

            PrepareDatabase(configuration.GetDatabaseConfiguration());
            PopulateDatabase(configuration.GetAchievementDatabaseConfiguration());
        }
        #endregion

        #region Functions
        /// <summary>
        /// Retrieves an instance of the achievement data access.
        /// </summary>
        /// <returns>Returns an achievement data access object.</returns>
        public IAchievementDAL GetAchievementDataAccess()
        {
            return _connectionMethodFactory.GetAchievementDataAccess();
        }

        /// <summary>
        /// Retrieves an instance of the achievement progression data access.
        /// </summary>
        /// <returns>Returns an achievement progression data access object.</returns>
        public IAchievementProgressionDAL GetAchievementProgressionDataAccess()
        {
            return _connectionMethodFactory.GetAchievementProgressionDataAccess();
        }
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

        #region Dispose
        /// <summary>
        /// Return this object back into the connection method factory object pool.
        /// </summary>
        public void Dispose()
        {
            ConnectionMethodFactoryPool.GetInstance().ReleaseConnectionMethodFactory(this);
        }
        #endregion
        #endregion
    }
}
