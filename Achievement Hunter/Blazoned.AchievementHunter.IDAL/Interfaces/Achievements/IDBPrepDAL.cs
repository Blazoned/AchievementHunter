﻿using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IDBPrepDAL : IConnectable
    {
        #region Functions
        #region Get Data
        /// <summary>
        /// Check if the achievement database extention has been correctly prepared and build.
        /// </summary>
        /// <param name="databaseInfo">The information with which to check the database extention build.</param>
        /// <returns>Returns true if the database conforms with the database informaton provided.</returns>
        bool IsDatabaseCreated(DatabaseInfoStruct databaseInfo);
        #endregion

        #region Create Data
        /// <summary>
        /// Prepare and build the achievement database extention.
        /// </summary>
        /// <param name="databaseInfo">The information with which to prepare and build the database extention.</param>
        /// <returns>Returns true if the database extention has been successfully build.</returns>
        bool PrepareDatabase(DatabaseInfoStruct databaseInfo);
        #endregion
        #endregion
    }
}
