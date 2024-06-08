using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Repository.DbExtracts
{
    public class BaseDbExtractRepo
    {
        protected ISqlSugarClient _client;
        public BaseDbExtractRepo(DbType dbType, string dbConnectionString)
        {
            _client = new SqlSugarClient(new ConnectionConfig
            {
                DbType = dbType,
                ConnectionString = dbConnectionString
            });
        }

        public async Task<List<string>> GetAsync(string sql)
        {
            var data = await _client.SqlQueryable<dynamic>(sql).ToListAsync();
            List<string> lst = new List<string>();
            foreach (var item in data)
            {
                lst.Add(item);
            }
            return lst;
        }

        public async Task<List<T>> GetAsync<T>(string sql) where T : class, new()
        {
            var data = await _client.SqlQueryable<T>(sql).ToListAsync();
            List<T> lst = new List<T>();
            foreach (var item in data)
            {
                lst.Add(item);
            }
            return lst;
        }
    }
}
