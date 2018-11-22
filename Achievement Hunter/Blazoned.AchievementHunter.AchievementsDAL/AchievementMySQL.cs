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
        public AchievementMySQL(string connectionString)
            : base(connectionString)
        {

        }

        public bool CreateAchievement(AchievementStruct achievement)
        {
            throw new NotImplementedException();
        }

        public bool DeleteAchievement(AchievementStruct achievement)
        {
            throw new NotImplementedException();
        }

        public bool IsPopulated(IEnumerable<AchievementStruct> achievements)
        {
            throw new NotImplementedException();
        }

        public bool PopulateDatabase(AchievementStruct[] achievements)
        {
            throw new NotImplementedException();
        }
    }
}
