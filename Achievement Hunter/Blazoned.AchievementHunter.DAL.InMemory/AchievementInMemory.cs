using Blazoned.AchievementHunter.DAL.InMemory.Database;
using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Interfaces.Configuration;

namespace Blazoned.AchievementHunter.DAL.InMemory
{
    public class AchievementInMemory : IAchievementDAL
    {
        private IConfigurationDAL _configurationDAL;

        public AchievementInMemory(IConfigurationDAL configurationDAL)
        {
            this._configurationDAL = configurationDAL;
        }

        public bool IsPopulated()
        {
            return InMemoryDatabase.GetInstance().IsPopulated(_configurationDAL.GetAchievementDatabaseConfiguration());
        }

        public void CreateAchievement(AchievementEnt achievement, bool updateConfig = false)
        {
            InMemoryDatabase.GetInstance().CreateAchievement(achievement);
        }

        public void PopulateDatabase(bool overwrite = false)
        {
            InMemoryDatabase.GetInstance().PopulateDatabase(_configurationDAL.GetAchievementDatabaseConfiguration(),
                                                            overwrite);
        }

        public void DeleteAchievement(string achievementId, bool updateConfig = false)
        {
            InMemoryDatabase.GetInstance().DeleteAchievement(achievementId);
        }

        public void ResetAchievements()
        {
            InMemoryDatabase.GetInstance().DeleteAchievements();
        }
    }
}
