using Blazoned.AchievementHunter.DAL.InMemory.Database;
using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.InMemory
{
    public class AchievementInMemory : ConnectionInMemory, IAchievementDAL
    {
        public AchievementInMemory()
            : base()
        {

        }

        public bool IsPopulated(IEnumerable<AchievementEnt> achievements)
        {
            return InMemoryDatabase.GetInstance().IsPopulated(achievements);
        }

        public bool CreateAchievement(AchievementEnt achievement)
        {
            return InMemoryDatabase.GetInstance().CreateAchievement(achievement);
        }

        public bool PopulateDatabase(IEnumerable<AchievementEnt> achievements, bool overwrite = true)
        {
            return InMemoryDatabase.GetInstance().PopulateDatabase(achievements, overwrite);
        }

        public bool DeleteAchievement(string achievementId)
        {
            return InMemoryDatabase.GetInstance().DeleteAchievement(achievementId);
        }

        public bool DeleteAchievements()
        {
            return InMemoryDatabase.GetInstance().DeleteAchievements();
        }
    }
}
