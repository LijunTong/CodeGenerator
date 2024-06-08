using CodeGenerator.Data.Entity;
using CodeGenerator.Repository.Interface;
using SqlSugar;

namespace CodeGenerator.Repository
{
    public class CodeHisRepository : BaseRepository<CodeHis>, ICodeHisRepository
    {
        public CodeHisRepository(ISqlSugarClient client) : base(client)
        {
        }
    }
}
