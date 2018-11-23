using System;
using System.Data;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IConnectable : IDisposable
    {
        #region Functions
        #region Get Data
        /// <summary>
        /// Retrieve the database connection object.
        /// </summary>
        /// <returns>Returns the database connection object.</returns>
        IDbConnection GetConnection();
        #endregion
        #region Create Data
        /// <summary>
        /// Set the database connection.
        /// </summary>
        /// <param name="connectionString">The database connection string.</param>
        void SetConnection(string connectionString);
        #endregion
        #endregion
    }
}
