using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Interfaces.Configuration;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.MySQL
{
    public abstract class ConnectionMySQL : IConnectionDAL
    {
        #region Fields
        /// <summary>
        /// The configuration object for the database.
        /// </summary>
        private IConfigurationDAL _configurationDAL;
        /// <summary>
        /// The connection object for the database.
        /// </summary>
        private IDbConnection _dbConnection;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate a connection object.
        /// </summary>
        /// <param name="configurationDAL">The configuration data acces object.</param>
        public ConnectionMySQL(IConfigurationDAL configurationDAL)
        {
            this._configurationDAL = configurationDAL;
            this._dbConnection = new MySqlConnection(_configurationDAL.GetConnection());
        }
        #endregion

        #region Functions
        #region Connection
        /// <summary>
        /// Open the database connection.
        /// </summary>
        /// <returns>Returns the open database connection object.</returns>
        public IDbConnection OpenConnection()
        {
            if (_dbConnection.State != ConnectionState.Open)
                _dbConnection.Open();

            return _dbConnection;
        }
        /// <summary>
        /// Closes the database connection.
        /// </summary>
        public void CloseConnection()
        {
            _dbConnection.Close();
        }
        #endregion

        #region Disposable
        /// <summary>
        /// Disposes of the object.
        /// </summary>
        public void Dispose()
        {
            CloseConnection();
        }
        #endregion
        #endregion
    }
}
