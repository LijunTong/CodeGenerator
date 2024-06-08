using CodeGenerator.Data.Entity.DatabaseEntity;
using CodeGenerator.Repository.Interface;
using Jt.Common.Tool.Helper;
using SqlSugar;
using DbTableInfo = CodeGenerator.Data.Entity.DatabaseEntity.DbTableInfo;

namespace CodeGenerator.Repository.DbExtracts
{
    public class PostgreSQLDbExtractRepo : BaseDbExtractRepo,IDbExtractRepo
    {
        public PostgreSQLDbExtractRepo(string dbConnectionString):base(DbType.PostgreSQL,dbConnectionString)
        {
        }

        public async Task<List<DbInfo>> GetDataBasesAsync()
        {
            string sql = "select schema_name as DataBase from information_schema.schemata where schema_name not in ('pg_toast','pg_catalog','information_schema');";
            return await GetAsync<DbInfo>(sql);
        }

        public async Task<List<DbTableInfo>> GetTableNamesAsync(string dbName)
        {
            string sql = $"select table_name as TableName  from information_schema.tables where table_schema='{dbName}'";
            return await GetAsync<DbTableInfo>(sql);
        }

        public async Task<List<DbFieldInfo>> GetDbFieldsAsync(string dbName, string tableName)
        {
            string sql = @$"select ordinal_position as Colorder,column_name as FieldName,udt_name as FieldDbType,
                coalesce(character_maximum_length,numeric_precision,-1) as Length,numeric_scale as DecimalPoint,
                is_nullable as IsNotNull,column_default as DefaultVal,
                case when is_identity = 'YES' then 1 else 0 end as IsIncrement, 
                case when b.pk_name is null then 0 else 1 end as IsPrimaryKey,c.DeText as FieldDes
                from information_schema.columns 
                left join (
                 select pg_attr.attname as colname,pg_constraint.conname as pk_name from pg_constraint 
                 inner join pg_class on pg_constraint.conrelid = pg_class.oid 
                 inner join pg_attribute pg_attr on pg_attr.attrelid = pg_class.oid and pg_attr.attnum = pg_constraint.conkey[1] 
                 inner join pg_type on pg_type.oid = pg_attr.atttypid
                 where pg_class.relname = '{tableName}' and pg_constraint.contype='p' 
                ) b on b.colname = information_schema.columns.column_name
                left join (
                 select attname,description as DeText from pg_class
                 left join pg_attribute pg_attr on pg_attr.attrelid= pg_class.oid
                 left join pg_description pg_desc on pg_desc.objoid = pg_attr.attrelid and pg_desc.objsubid=pg_attr.attnum
                 where pg_attr.attnum>0 and pg_attr.attrelid=pg_class.oid and pg_class.relname='{tableName}'
                )c on c.attname = information_schema.columns.column_name
                where table_schema='{dbName}' and table_name='{tableName}' order by ordinal_position asc";
            var data = await GetAsync<DbFieldInfo>(sql);
            foreach (var item in data)
            {
                item.FieldModelName = NamedHelper.ToPascal(item.FieldName);
                item.FieldModelNameCamel = NamedHelper.ToCamelCase(item.FieldName);
                item.FieldModelType = Constrant.DicPostgreSQLFieldMap[item.FieldDbType];
            }
            return data;
        }
    }
}
