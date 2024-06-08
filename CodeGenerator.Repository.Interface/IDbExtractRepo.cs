using CodeGenerator.Data.Entity.DatabaseEntity;

namespace CodeGenerator.Repository.Interface
{
    public interface IDbExtractRepo
    {
        Task<List<DbInfo>> GetDataBasesAsync();

        Task<List<DbTableInfo>> GetTableNamesAsync(string dbName);

        Task<List<DbFieldInfo>> GetDbFieldsAsync(string dbName, string tableName);
    }
}
