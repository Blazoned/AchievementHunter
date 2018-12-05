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
        #region Fields
        /// <summary>
        /// Contains the path to the folder which holds the configuration files.
        /// </summary>
        private string _configPath;
        #endregion

        #region Constructor
        /// <summary>
        /// Instantiates a data access configuration object.
        /// </summary>
        public ConfigurationDAL()
        {
            string assemblyPath = this.GetType().Assembly.Location;

            this._configPath = assemblyPath.Remove(assemblyPath.LastIndexOf('\\')) + @"\config\";
        }
        #endregion

        #region Functions
        /// <summary>
        /// Get the achievement configuration for a database.
        /// </summary>
        /// <returns>Returns the achievements to push to the database.</returns>
        public IEnumerable<AchievementEnt> GetAchievementDatabaseConfiguration()
        {
            return ReadJsonFile<List<AchievementEnt>>("achievement.config.json");
        }

        /// <summary>
        /// Get the connection and its database type.
        /// </summary>
        /// <returns>Returns struct with connection information.</returns>
        public string GetConnection()
        {
            dynamic databaseInfoJson = ReadJsonFile<dynamic>("data-access.config.json");

            return ParseConnectionString(databaseInfoJson.connection);
        }

        /// <summary>
        /// Get the database configuration.
        /// </summary>
        /// <returns>Returns the configuration for the database.</returns>
        public DatabaseInfoDataStruct GetDatabaseConfiguration()
        {
            dynamic databaseInfoJson = ReadJsonFile<dynamic>("data-access.config.json");

            return new DatabaseInfoDataStruct(databaseInfoJson.database.linkTable,
                                              databaseInfoJson.database.achievementTable,
                                              databaseInfoJson.database.userTable,
                                              databaseInfoJson.database.userKey);
        }

        public void AddAchievement(AchievementEnt achievement)
        {
            List<AchievementEnt> achievementConfigJson = ReadJsonFile<List<AchievementEnt>>("achievement.config.json");

            achievementConfigJson.Add(achievement);

            WriteJsonFile("achievement.config.json", achievementConfigJson);
        }

        public void RemoveAchievement(string achievementId)
        {
            List<AchievementEnt> achievementConfigJson = ReadJsonFile<List<AchievementEnt>>("achievement.config.json");

            var achievement = achievementConfigJson.Find(achieve => achieve.id == achievementId);
            achievementConfigJson.Remove(achievement);

            WriteJsonFile("achievement.config.json", achievementConfigJson);
        }
        #endregion

        #region Methods
        /// <summary>
        /// Parse a json file into a json object.
        /// </summary>
        /// <typeparam name="T">The object type to deserialise with.</typeparam>
        /// <param name="file">The path of the to-read file.</param>
        /// <returns>Returns the json object stored in the file.</returns>
        private T ReadJsonFile<T>(string file)
        {
            using (StreamReader r = new StreamReader(_configPath + file))
            {
                return JsonConvert.DeserializeObject<T>(r.ReadToEnd());
            }
        }

        public void WriteJsonFile(string file, object obj)
        {
            using (StreamWriter w = new StreamWriter(_configPath + file))
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
