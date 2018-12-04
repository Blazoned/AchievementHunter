using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using System.Data;

namespace Blazoned.AchievementHunter.DAL.InMemory
{
    public abstract class ConnectionInMemory : IConnectable
    {
        public ConnectionInMemory()
        {
            
        }

        public IDbConnection OpenConnection()
        {
            return null;
        }

        public void CloseConnection()
        {

        }

        public void Dispose()
        {
            
        }
    }
}
