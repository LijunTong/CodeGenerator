using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Repository.Interface;
using Jt.Common.Tool.DI;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Repository
{
    public class CodeSchemeDetialsRepository : BaseRepository<CodeSchemeDetials>, ICodeSchemeDetialsRepository, ITransientDIInterface<ICodeSchemeDetialsRepository>
    {
        public CodeSchemeDetialsRepository(ISqlSugarClient client) : base(client)
        {
        }

        public async Task<List<CodeSchemeDetialsDto>> GetCodeSchemeDetialsAsync(string schemeId)
        {
            string sql = $"SELECT cs.id,cs.file_name as filename,ct.Name as TempName,cs.temp_id as tempid FROM code_scheme_detials cs LEFT JOIN code_temp ct ON cs.temp_id = ct.id WHERE cs.is_del = 0 and gen_scheme_id='{schemeId}'";

            return await _clinet.SqlQueryable<CodeSchemeDetialsDto>(sql).ToListAsync();
        }
    }
}
