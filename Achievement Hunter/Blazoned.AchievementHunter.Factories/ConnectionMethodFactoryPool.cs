using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.Factories
{
    public class ConnectionMethodFactoryPool
    {
        #region Fields
        private static ConnectionMethodFactoryPool _instance;

        private List<ConnectionMethodFactoryProxy> _availableProxies;
        private Dictionary<ConnectionMethodFactoryProxy, DateTime> _inUseProxies;
        #endregion

        #region Constructors
        private ConnectionMethodFactoryPool()
        {
            Task.Run(RemoveUnreleasedProxies);
        }
        #endregion

        #region Functions
        /// <summary>
        /// Get an instance of the ConnectionMethodFactoryPool.
        /// </summary>
        /// <returns>Returns a ConnectionMethodFactoryPool object.</returns>
        public static ConnectionMethodFactoryPool GetInstance()
        {
            if (_instance == null)
            {
                _instance = new ConnectionMethodFactoryPool();
            }

            return _instance;
        }

        /// <summary>
        /// Retrieve a connection method factory from the object pool.
        /// </summary>
        /// <returns>Returns a connection method factory object.</returns>
        public ConnectionMethodFactoryProxy GetConnectionMethodFactory()
        {
            ConnectionMethodFactoryProxy methodFactory =
                _availableProxies.Count == 0 ?
                new ConnectionMethodFactoryProxy() :
                _availableProxies[0];

            _availableProxies.Remove(methodFactory);

            _inUseProxies.Add(methodFactory, DateTime.Now);

            return methodFactory;
        }
        /// <summary>
        /// Release a connection method factory to the object pool.
        /// </summary>
        /// <param name="connectionMethodFactory">The connection method factory which to release.</param>
        public void ReleaseConnectionMethodFactory(ConnectionMethodFactoryProxy connectionMethodFactory)
        {
            if (_inUseProxies.Remove(connectionMethodFactory))
                _availableProxies.Add(connectionMethodFactory);
        }
        #endregion

        #region Method
        /// <summary>
        /// Asynchronously remove any unreleased proxies if they are in use for too long (5 minutes) each 10 minutes.
        /// </summary>
        private async Task RemoveUnreleasedProxies()
        {
            while (true)
            {
                await Task.Delay(TimeSpan.FromMinutes(10));

                IEnumerable<KeyValuePair<ConnectionMethodFactoryProxy, DateTime>> overdueResources =
                    _inUseProxies.Where(entry => entry.Value > DateTime.Now.AddMinutes(5));

                foreach (var inUseProxy in overdueResources)
                {
                    _inUseProxies.Remove(inUseProxy.Key);
                }
            }
        }
        #endregion
    }
}
