using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.MySQL
{
    public class DBPrepMySQL : ConnectionMySQL, IDBPrepDAL
    {
        public DBPrepMySQL(string connectionString)
            : base(connectionString)
        {

        }

        public bool IsDatabaseCreated(DatabaseInfoStruct databaseInfo)
        {
            throw new NotImplementedException();
        }

        public bool PrepareDatabase(DatabaseInfoStruct databaseInfo)
        {
            throw new NotImplementedException();
        }
    }
}
