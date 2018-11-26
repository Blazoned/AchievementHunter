using Blazoned.AchievementHunter.Entities;
using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.MySQL
{
    public class AchievementMySQL : ConnectionMySQL, IAchievementDAL
    {
        public AchievementMySQL(string connectionString)
            : base(connectionString)
        {

        }

        public bool IsPopulated(IEnumerable<AchievementEnt> achievements)
        {
            throw new NotImplementedException();
        }

        public bool CreateAchievement(AchievementEnt achievement)
        {
            throw new NotImplementedException();
        }

        public bool PopulateDatabase(IEnumerable<AchievementEnt> achievements, bool overwrite = true)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAchievement(string achievementId)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAchievements()
        {
            throw new NotImplementedException();
        }
    }
}
