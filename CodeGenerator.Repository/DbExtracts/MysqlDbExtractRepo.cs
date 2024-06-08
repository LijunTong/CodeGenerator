using CodeGenerator.Data.Entity.DatabaseEntity;
using CodeGenerator.Repository.Interface;
using Jt.Common.Tool.Extension;
using Jt.Common.Tool.Helper;
using SqlSugar;
using DbTableInfo = CodeGenerator.Data.Entity.DatabaseEntity.DbTableInfo;

namespace CodeGenerator.Repository.DbExtracts
{
    public class MysqlDbExtractRepo : BaseDbExtractRepo, IDbExtractRepo
    {
        public MysqlDbExtractRepo(string dbConnectionString) : base(DbType.MySql, dbConnectionString)
        {
        }

        public async Task<List<DbInfo>> GetDataBasesAsync()
        {
            string sql = "SHOW DATABASES";
            return await GetAsync<DbInfo>(sql);
        }

        public async Task<List<DbTableInfo>> GetTableNamesAsync(string dbName)
        {
            string sql = $"select table_name as 'TableName'  from information_schema.tables where table_schema='{dbName}'";
            return await GetAsync<DbTableInfo>(sql);
        }

        public async Task<List<DbFieldInfo>> GetDbFieldsAsync(string dbName, string tableName)
        {
            string sql = @$"SELECT COLUMN_NAME AS 'FieldName',DATA_TYPE AS `FieldDbType`,CHARACTER_MAXIMUM_LENGTH AS `FieldLength`,NUMERIC_PRECISION AS `NumberLength`,NUMERIC_SCALE AS `DecimalPoint`,CASE WHEN IS_NULLABLE = 'YES' THEN 1 ELSE 0 END AS `IsNotNull`, CASE WHEN EXTRA = 'auto_increment' THEN 1 ELSE 0 END AS `IsIncrement`,COLUMN_DEFAULT AS `DefaultValue`,COLUMN_COMMENT AS `FieldDes`
                FROM information_schema.COLUMNS WHERE TABLE_SCHEMA = '{dbName}' AND TABLE_NAME = '{tableName}' ORDER BY ordinal_position";
            var data = await GetAsync<DbFieldInfo>(sql);
            foreach (var item in data)
            {
                item.FieldModelName = NamedHelper.ToPascal(item.FieldName);
                item.FieldModelNameCamel = NamedHelper.ToCamelCase(item.FieldName);
                Constrant.DicMysqlFieldMap.TryGetValue(item.FieldDbType, out string val);//[item.FieldDbType];
                item.FieldModelType = val.IsNullOrWhiteSpace() ? "string" : val;
            }
            return data;
        }
    }
}
