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
    public class DBPrepMySQL : IDBPrepDAL
    {
        #region Fields
        /// <summary>
        /// The connection to use.
        /// </summary>
        private IConnectionDAL _connectable;
        /// <summary>
        /// The configuration data access to use.
        /// </summary>
        private IConfigurationDAL _configurationDAL;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate the database preparation data access object.
        /// </summary>
        /// <param name="connectable">The connection object.</param>
        /// <param name="configurationDAL">The configuration data access.</param>
        public DBPrepMySQL(IConnectionDAL connectable, IConfigurationDAL configurationDAL)
        {
            this._connectable = connectable;
            this._configurationDAL = configurationDAL;
        }
        #endregion

        #region Functions
        /// <summary>
        /// Check if the achievement database extention has been correctly prepared and build.
        /// </summary>
        /// <returns>Returns true if the database has already been populated.</returns>
        public bool IsDatabaseCreated()
        {
            var dbConfiguration = _configurationDAL.GetDatabaseConfiguration();

            return IsDatabasePrepared(dbConfiguration.achievementTable,
                                      dbConfiguration.linkTable,
                                      dbConfiguration.userTable,
                                      dbConfiguration.userKey);
        }
        /// <summary>
        /// Prepare and build the achievement database extention.
        /// </summary>
        public void PrepareDatabase()
        {
            var dbConfiguration = _configurationDAL.GetDatabaseConfiguration();

            if (!IsDatabasePrepared(dbConfiguration.achievementTable,
                                    dbConfiguration.linkTable,
                                    dbConfiguration.userTable,
                                    dbConfiguration.userKey))
            {
                Prepare(dbConfiguration.achievementTable,
                        dbConfiguration.linkTable,
                        dbConfiguration.userTable,
                        dbConfiguration.userKey);
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Check if the database has been prepared or not.
        /// </summary>
        /// <param name="achievementTable">The achievement table name.</param>
        /// <param name="linkTable">The link table name.</param>
        /// <param name="userTable">The user table name.</param>
        /// <param name="userKey">The user table primary key.</param>
        /// <returns>Returns true if the database is already build.</returns>
        private bool IsDatabasePrepared(string achievementTable, string linkTable, string userTable, string userKey)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Prepare the database.
        /// </summary>
        /// <param name="achievementTable">The achievement table name.</param>
        /// <param name="linkTable">The link table name.</param>
        /// <param name="userTable">The user table name.</param>
        /// <param name="userKey">The user table primary key.</param>
        private void Prepare(string achievementTable, string linkTable, string userTable, string userKey)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
