using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    // TODO: Connection Singleton that varies depending on the DAL
    public interface IConnectable
    {
        void SetConnection(DatabaseInfoStruct databaseInfo);
    }
}
