using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Repository
{
    public class BaseRepository<T> : SimpleClient<T> where T : class, new()
    {
        protected ISqlSugarClient _clinet;
        public BaseRepository(ISqlSugarClient client) : base(client)
        {
            _clinet = client;
        }
    }
}
