using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter
{
    public class AchievementManager
    {
        #region Fields
        public static AchievementManager _this;
        public List<Achievement> achievements;
        #endregion

        #region Constructor
        private AchievementManager()
        {

        }
        #endregion

        public AchievementManager GetInstance()
        {
            if (_this == null)
            {
                _this = new AchievementManager();
            }
            return _this;
        }
    }
}
