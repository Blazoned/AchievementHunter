using Blazoned.AchievementHunter.IDAL.Interfaces.Config;
using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.Config
{
    public class ConfigDAL : IConfigDAL
    {
        public AchievementStruct[] LoadAchievements()
        {
            throw new NotImplementedException();
        }

        public DatabaseInfoStruct LoadDataAccessConfiguration()
        {
            throw new NotImplementedException();
        }
    }
}
