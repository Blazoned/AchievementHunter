using System;

namespace Blazoned.AchievementHunter.Factories
{
    public class DatabaseAttribute : Attribute
    {
        #region Fields
        /// <summary>
        /// Gets the database type.
        /// </summary>
        public string DatabaseType { get; private set; }
        #endregion

        #region Constructor
        public DatabaseAttribute(string databaseType)
        {
            this.DatabaseType = databaseType;
        }
        #endregion
    }
}
