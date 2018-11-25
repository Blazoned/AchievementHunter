using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.InMemory
{
    public class DBPrepInMemory : ConnectionInMemory, IDBPrepDAL
    {
        public DBPrepInMemory()
            : base()
        {

        }

        public bool IsDatabaseCreated(DatabaseInfoDataStruct databaseInfo)
        {
            return true;
        }

        public bool PrepareDatabase(DatabaseInfoDataStruct databaseInfo)
        {
            return true;
        }
    }
}
