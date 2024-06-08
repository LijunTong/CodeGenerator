using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Lib.Options
{
    public class AppSetting
    {
        public string Title { get; set; }
        public DbClient SqlSugar {  get; set; }
    }
    public class DbClient
    {
        public string DbType { get; set; }
        public string ConnectionString { get; set; }
        public bool IsAutoCloseConnection { get; set; }
    }

}
