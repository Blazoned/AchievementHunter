using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Structs.Config
{
    public struct ConnectionStruct
    {
        #region Fields
        /// <summary>
        /// The location of backup files, in case the database connection is faulty.
        /// </summary>
        public readonly string location;
        /// <summary>
        /// The database connection string. This connection string should be encrypted.
        /// </summary>
        public readonly string connection;
        /// <summary>
        /// The internal database information. The information about the database should be encrypted.
        /// </summary>
        public readonly DatabaseInfoStruct databaseInfo;
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiate a connection struct.
        /// </summary>
        /// <param name="location">The location of backup files, in case the database connection is faulty.</param>
        /// <param name="connection">The database connection string. This connection string should be encrypted.</param>
        /// <param name="databaseInfo">The internal database information. The information about the database should be encrypted.</param>
        public ConnectionStruct(string location, string connection, DatabaseInfoStruct databaseInfo)
        {
            this.location = location;
            this.connection = connection;
            this.databaseInfo = databaseInfo;
        }
        #endregion
    }
}
