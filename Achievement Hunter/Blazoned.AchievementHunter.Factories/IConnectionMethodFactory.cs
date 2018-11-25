using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;

namespace Blazoned.AchievementHunter.Factories
{
    public interface IConnectionMethodFactory
    {        
        IDBPrepDAL GetDBPreparationDataAccess();
        IAchievementDAL GetAchievementDataAccess();
        IAchievementProgressionDAL GetAchievementProgressionDataAccess();
    }
}
