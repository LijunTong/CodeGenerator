using CodeGenerator.Data.Entity;
using CodeGenerator.Repository.Interface;
using SqlSugar;

namespace CodeGenerator.Repository
{
    public class CodeDbRepository : BaseRepository<CodeDb>, ICodeDbRepository
    {
        public CodeDbRepository(ISqlSugarClient client) : base(client)
        {
        }
    }
}
