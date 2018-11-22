using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.MySQL
{
    public class AchievementProgressionMySQL : ConnectionMySQL, IAchievementProgressionDAL
    {
        public AchievementProgressionMySQL(string connectionString)
            : base(connectionString)
        {

        }

        public bool DeleteUserData(string userId)
        {
            throw new NotImplementedException();
        }

        public AchievementProgressionStruct GetAchievementProgression(string userId)
        {
            throw new NotImplementedException();
        }

        public AchievementProgressionStruct[] GetAchievementProgressions(string[] userIds)
        {
            throw new NotImplementedException();
        }

        public AchievementProgressionStruct[] GetAchievementProgressions(int count)
        {
            throw new NotImplementedException();
        }

        public bool UpdateAchievementProgression(AchievementProgressionStruct progression)
        {
            throw new NotImplementedException();
        }
    }
}
