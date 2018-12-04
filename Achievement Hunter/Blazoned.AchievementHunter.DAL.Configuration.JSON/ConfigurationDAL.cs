using Blazoned.AchievementHunter.Entities;
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
        public IEnumerable<AchievementEnt> GetAchievementDatabaseConfiguration()
        {
            return ReadJsonFile<List<AchievementEnt>>("config/data-access.config.json");
        }

        /// <summary>
        /// Get the connection and its database type.
        /// </summary>
        /// <returns>Returns struct with connection information.</returns>
        public string GetConnection()
        {
            dynamic databaseInfoJson = ReadJsonFile<dynamic>("config/data-access.config.json");

            return ParseConnectionString(databaseInfoJson.connection);
        }

        /// <summary>
        /// Get the database configuration.
        /// </summary>
        /// <returns>Returns the configuration for the database.</returns>
        public DatabaseInfoDataStruct GetDatabaseConfiguration()
        {
            dynamic databaseInfoJson = ReadJsonFile<dynamic>("config/data-access.config.json");

            return new DatabaseInfoDataStruct(databaseInfoJson.database.linkTable,
                                          databaseInfoJson.database.achievementTable,
                                          databaseInfoJson.database.userTable,
                                          databaseInfoJson.database.userKey);
        }

        public void AddAchievement(AchievementEnt achievement)
        {
            List<AchievementEnt> achievementConfigJson = ReadJsonFile<List<AchievementEnt>>("config/data-access.config.json");

            achievementConfigJson.Add(achievement);

            WriteJsonFile("config/data-access.config.json", achievementConfigJson);
        }

        public void RemoveAchievement(string achievementId)
        {
            List<AchievementEnt> achievementConfigJson = ReadJsonFile<List<AchievementEnt>>("config/data-access.config.json");

            var achievement = achievementConfigJson.Find(achieve => achieve.id == achievementId);
            achievementConfigJson.Remove(achievement);

            WriteJsonFile("config/data-access.config.json", achievementConfigJson);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parse a json file into a json object.
        /// </summary>
        /// <typeparam name="T">The object type to deserialise with.</typeparam>
        /// <param name="path">The path of the to-read file.</param>
        /// <returns>Returns the json object stored in the file.</returns>
        private dynamic ReadJsonFile<T>(string path)
        {
            using (StreamReader r = new StreamReader(path))
            {
                return JsonConvert.DeserializeObject<T>(r.ReadToEnd());
            }
        }

        public void WriteJsonFile(string path, object obj)
        {
            using (StreamWriter w = new StreamWriter(path))
            {
                JsonConvert.SerializeObject(obj);

                w.Write(obj);
                w.Flush();
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
