using CodeGenerator.Data;
using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Services.Interface
{
    public interface ICodeSchemeDetialsSvc : IBaseSvc<CodeSchemeDetials>
    {
        Task<List<CodeSchemeDetialsDto>> GetDetialsBySchemeId(string schemeId);
    }
}
