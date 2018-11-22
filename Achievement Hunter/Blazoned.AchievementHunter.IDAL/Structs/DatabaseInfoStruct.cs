using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Structs
{
    public struct DatabaseInfoStruct
    {
        #region Fields
        /// <summary>
        /// The link table that is responsible for keeping track of a user's achievements.
        /// </summary>
        public readonly string linkTable;
        /// <summary>
        /// The achievement table that is responsible for storing the globally available achievements.
        /// </summary>
        public readonly string achievementTable;
        /// <summary>
        /// The user table that contains the users which to be linked to their individual achievements progression.
        /// </summary>
        public readonly string userTable;
        /// <summary>
        /// The primary key of the user table to use within the link table to match the user to an achievement.
        /// </summary>
        public readonly string userKey;
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiate a database information struct.
        /// </summary>
        /// <param name="linkTable">The link table that is responsible for keeping track of a user's achievements.</param>
        /// <param name="achievementTable">The achievement table that is responsible for storing the globally available achievements.</param>
        /// <param name="userTable">The user table that contains the users which to be linked to their individual achievements progression.</param>
        /// <param name="userKey">The primary key of the user table to use within the link table to match the user to an achievement.</param>
        public DatabaseInfoStruct(string linkTable, string achievementTable, string userTable, string userKey)
        {
            this.linkTable = linkTable;
            this.achievementTable = achievementTable;
            this.userTable = userTable;
            this.userKey = userKey;
        }
        #endregion
    }
}
