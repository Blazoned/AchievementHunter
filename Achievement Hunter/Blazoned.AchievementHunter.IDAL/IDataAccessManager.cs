using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.IDAL
{
    public interface IDataAccessManager
    {
        /// <summary>
        /// Gets an instance of the data access manager.
        /// </summary>
        /// <returns>Returns an instance of the data access manager.</returns>
        IDataAccessManager GetInstance();
    }
}