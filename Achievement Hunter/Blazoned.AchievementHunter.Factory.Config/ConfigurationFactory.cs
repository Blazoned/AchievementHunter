using Blazoned.AchievementHunter.IDAL.Interfaces.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.Factory.Config
{
    public class ConfigurationFactory
    {
        #region Fields
        /// <summary>
        /// The configuration factory instance of this singleton.
        /// </summary>
        private static ConfigurationFactory _instance;
        #endregion

        #region Constructors
        /// <summary>
        /// Instantiate the configuration factory.
        /// </summary>
        protected ConfigurationFactory()
        {

        }
        #endregion

        #region Functions
        /// <summary>
        /// Get an instance of the configuration factory.
        /// </summary>
        /// <returns>Returns an instance of the configuration factory.</returns>
        public static ConfigurationFactory GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConfigurationFactory();
            }
            return _instance;
        }

        /// <summary>
        /// Retrieve the configuration DAL using the json configuration files.
        /// </summary>
        /// <returns>Returns a configuration DAL object.</returns>
        public IConfigDAL GetJSONConfigurationDAL()
        {

        }
        #endregion

        #region Methods
        #endregion
    }
}
