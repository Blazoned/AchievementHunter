using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;

namespace Blazoned.AchievementHunter.DAL.InMemory
{
    public class DBPrepInMemory : IDBPrepDAL
    {
        public DBPrepInMemory()
        {

        }

        public bool IsDatabaseCreated()
        {
            return true;
        }

        public void PrepareDatabase()
        {
            
        }
    }
}
