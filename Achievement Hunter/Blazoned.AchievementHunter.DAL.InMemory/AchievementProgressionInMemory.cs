using Blazoned.AchievementHunter.DAL.InMemory.Database;
using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.InMemory
{
    public class AchievementProgressionInMemory : ConnectionInMemory, IAchievementProgressionDAL
    {
        public AchievementProgressionInMemory()
            : base()
        {

        }

        public IEnumerable<UserAchievementEnt> GetAchievementProgression(string userId)
        {
            return InMemoryDatabase.GetInstance().GetAchievementProgression(userId);
        }

        public bool UpdateAchievementProgression(UserAchievementEnt progression)
        {
            return InMemoryDatabase.GetInstance().UpdateAchievementProgression(progression);
        }

        public bool DeleteUserData(string userId)
        {
            return InMemoryDatabase.GetInstance().DeleteUserData(userId);
        }
    }
}
