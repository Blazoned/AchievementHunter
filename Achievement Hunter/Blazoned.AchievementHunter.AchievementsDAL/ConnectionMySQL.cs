using Blazoned.AchievementHunter.IDAL.Interfaces.Achievements;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blazoned.AchievementHunter.DAL.MySQL
{
    public abstract class ConnectionMySQL : IConnectable
    {
        protected string _connectionString;

        public ConnectionMySQL(string connectionString)
        {
            SetConnection(connectionString);
        }

        public IDbConnection GetConnection()
        {
            return new MySqlConnection(_connectionString);
        }

        public void SetConnection(string connectionString)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }
    }
}
