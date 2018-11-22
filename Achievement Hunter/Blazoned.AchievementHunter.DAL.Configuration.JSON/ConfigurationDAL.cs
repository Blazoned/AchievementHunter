using Blazoned.AchievementHunter.IDAL.Interfaces.Configuration;
using Blazoned.AchievementHunter.IDAL.Structs;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Blazoned.AchievementHunter.DAL.Configuration
{
    public class ConfigurationDAL : IConfigurationDAL
    {
        #region Functions
        /// <summary>
        /// Get the achievement configuration for a database.
        /// </summary>
        /// <returns>Returns the achievements to push to the database.</returns>
        public IEnumerable<AchievementStruct> GetAchievementDatabaseConfiguration()
        {
            dynamic achievementConfigJson = ReadJsonFile("config/data-access.config.json");

            List<AchievementStruct> achievements = new List<AchievementStruct>();

            foreach(var achievement in achievementConfigJson.achievements)
            {
                achievements.Add(new AchievementStruct(
                    achievement.id,
                    achievement.title,
                    achievement.description,
                    achievement.score,
                    achievement.goal));
            }

            return achievements;
        }

        /// <summary>
        /// Get the connection and its database type.
        /// </summary>
        /// <returns>Returns struct with connection information.</returns>
        public ConnectionStruct GetConnection()
        {
            dynamic databaseInfoJson = ReadJsonFile("config/data-access.config.json");

            string connection = ParseConnectionString(databaseInfoJson.connection);

            return new ConnectionStruct(connection,
                                        databaseInfoJson.databaseType);
        }

        /// <summary>
        /// Get the database configuration.
        /// </summary>
        /// <returns>Returns the configuration for the database.</returns>
        public DatabaseInfoStruct GetDatabaseConfiguration()
        {
            dynamic databaseInfoJson = ReadJsonFile("config/data-access.config.json");

            return new DatabaseInfoStruct(databaseInfoJson.database.linkTable,
                                          databaseInfoJson.database.achievementTable,
                                          databaseInfoJson.database.userTable,
                                          databaseInfoJson.database.userKey);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parse a json file into a json object.
        /// </summary>
        /// <param name="path">The path of the to-read file.</param>
        /// <returns>Returns the json object stored in the file.</returns>
        private dynamic ReadJsonFile(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject(r.ReadToEnd());
            }
        }

        /// <summary>
        /// Decrypt a connection string or encrypt it and write it to the configuration file.
        /// </summary>
        /// <param name="connection">The connection string found in the configuration file (either encrypted or non-encrypted).</param>
        /// <returns>Returns a connection</returns>
        private string ParseConnectionString(string connection)
        {
            //TODO: Add encryption and decryption protocol.
            return connection;
        }
        #endregion
    }
}
