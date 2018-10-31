using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.MySQL
{
    [Database("MySQL")]
    public abstract class ConnectionMySQL : IConnectable
    {
        public void SetConnection(DatabaseInfoStruct databaseInfo)
        {
            throw new NotImplementedException();
        }
    }
}
