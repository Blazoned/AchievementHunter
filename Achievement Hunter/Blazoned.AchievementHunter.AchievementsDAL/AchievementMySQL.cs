using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Structs;
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
        public bool CreateAchievements(AchievementStruct[] connection)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAchievements()
        {
            throw new NotImplementedException();
        }
    }
}
