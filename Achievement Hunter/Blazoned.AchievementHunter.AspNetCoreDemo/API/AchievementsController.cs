using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Blazoned.AchievementHunter.Entities;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Blazoned.AchievementHunter.AspNetCoreDemo.API
{
    [Route("api/[controller]")]
    public class AchievementsController : Controller
    {
        #region Fields
        private AchievementManager _achievementManager;
        #endregion

        #region Constructor
        public AchievementsController(AchievementManager achievementManager)
        {
            this._achievementManager = achievementManager;
        }
        #endregion

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<UserAchievementEnt> Get()
        {
            _achievementManager.LoadUserProgress("Blazoned");

            return _achievementManager["Blazoned"];
        }

        // GET api/<controller>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/increase
        [HttpPut("increase/{id}")]
        public void Put(string id, [FromBody]new { })
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
