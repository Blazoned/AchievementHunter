using Blazoned.AchievementHunter.IDAL.Structs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Achievements
{
    public interface IAchievementDAL : IConnectable
    {
        #region Create Data
        bool CreateAchievements(AchievementStruct[] connection);
        #endregion

        #region Delete Data
        bool DeleteAchievements();
        #endregion
    }
}
