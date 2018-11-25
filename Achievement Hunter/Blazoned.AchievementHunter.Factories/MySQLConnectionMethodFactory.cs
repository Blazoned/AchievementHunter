using Blazoned.AchievementHunter.DAL.MySQL;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;

namespace Blazoned.AchievementHunter.Factories
{
    [Database("MySQL")]
    internal class MySQLConnectionMethodFactory : IConnectionMethodFactory
    {
        #region Fields
        /// <summary>
        /// The database connection string.
        /// </summary>
        private string _connectionString;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate a MySQL Method Factory.
        /// </summary>
        /// <param name="connectionString">The connection string to use.</param>
        public MySQLConnectionMethodFactory(string connectionString)
        {
            this._connectionString = connectionString;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Retrieves an instance of the database preparation data access.
        /// </summary>
        /// <returns>Returns an database preparation data access object.</returns>
        public IDBPrepDAL GetDBPreparationDataAccess()
        {
            return new DBPrepMySQL(_connectionString);
        }

        /// <summary>
        /// Retrieves an instance of the achievement data access.
        /// </summary>
        /// <returns>Returns an achievement data access object.</returns>
        public IAchievementDAL GetAchievementDataAccess()
        {
            return new AchievementMySQL(_connectionString);
        }

        /// <summary>
        /// Retrieves an instance of the achievement progression data access.
        /// </summary>
        /// <returns>Returns an achievement progression data access object.</returns>
        public IAchievementProgressionDAL GetAchievementProgressionDataAccess()
        {
            return new AchievementProgressionMySQL(_connectionString);
        }
        #endregion
    }
}
