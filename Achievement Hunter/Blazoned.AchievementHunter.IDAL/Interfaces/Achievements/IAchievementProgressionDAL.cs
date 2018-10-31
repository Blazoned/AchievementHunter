using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IAchievementProgressionDAL : IConnectable
    {
        #region Get Data
        AchievementProgressionStruct GetAchievementProgression(string userId);
        AchievementProgressionStruct[] GetAchievementProgressions(string[] userIds);
        AchievementProgressionStruct[] GetAchievementProgressions(int count);
        #endregion

        #region Update Data
        bool UpdateAchievementProgression(AchievementProgressionStruct progression);
        #endregion

        #region Delete Data
        bool DeleteUserData(string userId);
        #endregion
    }
}
