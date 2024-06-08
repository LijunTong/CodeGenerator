using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Repository.Interface
{
    public interface ICodeSchemeDetialsRepository : IBaseRepository<CodeSchemeDetials>
    {
        Task<List<CodeSchemeDetialsDto>> GetCodeSchemeDetialsAsync(string schemeId);
    }
}
