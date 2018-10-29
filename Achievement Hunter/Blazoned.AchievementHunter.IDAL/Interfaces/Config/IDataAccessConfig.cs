using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Blazoned.AchievementHunter.IDAL.Structs.Config;

namespace Blazoned.AchievementHunter.IDAL.Interfaces.Config
{
    public interface IDataAccessConfig
    {
        ConnectionStruct[] LoadConfiguration();
        AchievementStruct LoadAchievements();
    }
}
