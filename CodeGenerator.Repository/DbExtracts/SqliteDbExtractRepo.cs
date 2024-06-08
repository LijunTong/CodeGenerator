using CodeGenerator.Data.Entity.DatabaseEntity;
using CodeGenerator.Repository.Interface;
using Jt.Common.Tool.Extension;
using Jt.Common.Tool.Helper;
using SqlSugar;
using System.Collections.Generic;
using DbTableInfo = CodeGenerator.Data.Entity.DatabaseEntity.DbTableInfo;

namespace CodeGenerator.Repository.DbExtracts
{
    public class SqliteDbExtractRepo : BaseDbExtractRepo, IDbExtractRepo
    {
        public SqliteDbExtractRepo(string dbConnectionString) : base(DbType.Sqlite, dbConnectionString)
        {
        }

        public async Task<List<DbInfo>> GetDataBasesAsync()
        {
            string sql = "SELECT 'main' AS DataBase";
            return await GetAsync<DbInfo>(sql);
        }

        public async Task<List<DbTableInfo>> GetTableNamesAsync(string dbName)
        {
            string sql = $"SELECT name AS 'TableName' FROM sqlite_master WHERE type='table'";
            return await GetAsync<DbTableInfo>(sql);
        }

        public async Task<List<DbFieldInfo>> GetDbFieldsAsync(string dbName, string tableName)
        {
            string sql = $"PRAGMA table_info('{tableName}')";
            var data = await GetAsync<dynamic>(sql);
            List<DbFieldInfo> datas = new List<DbFieldInfo>();
            foreach (var item in data)
            {
                var addItem = new DbFieldInfo();
                addItem.FieldName = item.name;
                addItem.FieldDbType = item.type;
                addItem.IsNotNull = item.notnull == 1;
                // addItem.IsIncrement = 1; // SQLite 自增列一般是 INTEGER PRIMARY KEY
                addItem.DefaultValue = item.dflt_value;
                addItem.IsPrimaryKey = item.pk == 1;
                addItem.FieldModelName = NamedHelper.ToPascal(addItem.FieldName);
                addItem.FieldModelNameCamel = NamedHelper.ToCamelCase(addItem.FieldName);
                Constrant.DicMysqlFieldMap.TryGetValue(addItem.FieldDbType, out string val);//[item.FieldDbType];
                addItem.FieldModelType = val.IsNullOrWhiteSpace() ? "string" : val;
                datas.Add(addItem);
            }
            return datas;
        }
    }
}
