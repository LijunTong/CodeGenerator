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
    public class CodeGenSchemeRepository : BaseRepository<CodeGenScheme>, ICodeGenSchemeRepository, ITransientDIInterface<ICodeGenSchemeRepository>
    {
        public CodeGenSchemeRepository(ISqlSugarClient client) : base(client)
        {
        }
    }
}
