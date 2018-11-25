using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.InMemory
{
    public abstract class ConnectionInMemory : IConnectable
    {
        public ConnectionInMemory()
        {
            
        }

        public IDbConnection GetConnection()
        {
            return null;
        }

        public void SetConnection(string connectionString)
        {
            
        }

        public void Dispose()
        {
            
        }
    }
}
