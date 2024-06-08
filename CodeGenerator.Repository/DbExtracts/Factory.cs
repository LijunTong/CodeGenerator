using CodeGenerator.Repository.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Repository.DbExtracts
{
    public class Factory
    {
        public static IDbExtractRepo GetDbExtractRepo(DbType dbType,string conStr)
        {
            switch (dbType)
            {
                case DbType.MySql:
                    return new MysqlDbExtractRepo(conStr);
                case DbType.SqlServer:
                    return new SqlServerDbExtractRepo(conStr);
                case DbType.PostgreSQL:
                    return new PostgreSQLDbExtractRepo(conStr);
                case DbType.Sqlite:
                    return new SqliteDbExtractRepo(conStr);
                default:
                    throw new NotImplementedException();
            }
        }
    }
}
