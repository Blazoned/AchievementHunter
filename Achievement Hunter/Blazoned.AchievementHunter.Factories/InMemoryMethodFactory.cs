using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.DAL.InMemory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.Factories
{
    [Database("InMemory")]
    internal class InMemoryMethodFactory : IConnectionMethodFactory
    {
        #region Fields
        /// <summary>
        /// The database connection string.
        /// </summary>
        private string _connectionString;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate an In-Memory Method Factory.
        /// </summary>
        /// <param name="connectionString">The connection string to use. Can be left empty and is only used to conform with the proxy's expectations (reflection).</param>
        public InMemoryMethodFactory(string connectionString)
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
            return new DBPrepInMemory();
        }

        /// <summary>
        /// Retrieves an instance of the achievement data access.
        /// </summary>
        /// <returns>Returns an achievement data access object.</returns>
        public IAchievementDAL GetAchievementDataAccess()
        {
            return new AchievementInMemory();
        }

        /// <summary>
        /// Retrieves an instance of the achievement progression data access.
        /// </summary>
        /// <returns>Returns an achievement progression data access object.</returns>
        public IAchievementProgressionDAL GetAchievementProgressionDataAccess()
        {
            return new AchievementProgressionInMemory();
        }
        #endregion
    }
}
