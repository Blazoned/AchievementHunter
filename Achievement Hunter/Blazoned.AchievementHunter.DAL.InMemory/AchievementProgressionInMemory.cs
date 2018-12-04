using Blazoned.AchievementHunter.DAL.InMemory.Database;
using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using System.Collections.Generic;

namespace Blazoned.AchievementHunter.DAL.InMemory
{
    public class AchievementProgressionInMemory : IAchievementProgressionDAL
    {
        public AchievementProgressionInMemory()
        {

        }

        public IEnumerable<UserAchievementEnt> GetAchievementProgression(string userId)
        {
            return InMemoryDatabase.GetInstance().GetAchievementProgression(userId);
        }

        public void UpdateAchievementProgression(UserAchievementEnt progression)
        {
            InMemoryDatabase.GetInstance().UpdateAchievementProgression(progression);
        }

        public void DeleteUserData(string userId)
        {
            InMemoryDatabase.GetInstance().DeleteUserData(userId);
        }
    }
}
