using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Structs
{
    public struct ConnectionStruct
    {
        #region Fields
        /// <summary>
        /// The connection string to connect to the database.
        /// </summary>
        public readonly string connectionString;
        /// <summary>
        /// The kind of database to connect to.
        /// </summary>
        public readonly string databaseType;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate a connection struct.
        /// </summary>
        /// <param name="connectionString">The connection string to connect to the database.</param>
        /// <param name="databaseType">The type of database to connect to.</param>
        public ConnectionStruct(string connectionString, string databaseType)
        {
            this.connectionString = connectionString;
            this.databaseType = databaseType;
        }
        #endregion
    }
}
