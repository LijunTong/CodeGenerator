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
    public class CodeTempRepository : BaseRepository<CodeTemp>, ICodeTempRepository, ITransientDIInterface<ICodeTempRepository>
    {
        public CodeTempRepository(ISqlSugarClient client) : base(client)
        {
        }
    }
}
