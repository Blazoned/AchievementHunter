using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.Factories
{
    public interface IConnectionMethodFactory
    {
        IDBPrepDAL GetDBPreparationDataAccess();
        IAchievementDAL GetAchievementDataAccess();
        IAchievementProgressionDAL GetAchievementProgressionDataAccess();
    }
}
