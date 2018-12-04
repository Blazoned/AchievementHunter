using System;
using System.Data;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IConnectable : IDisposable
    {
        #region Functions
        /// <summary>
        /// Open the database connection.
        /// </summary>
        /// <returns>Returns the open database connection object.</returns>
        IDbConnection OpenConnection();
        /// <summary>
        /// Closes the database connection.
        /// </summary>
        void CloseConnection();
        #endregion
    }
}
