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
    public interface ICodeGenSchemeSvc : IBaseSvc<CodeGenScheme>
    {
        Task<bool> AddCodeSchemeAsync(CodeGenSchemeDto codeSchemeDto, List<CodeSchemeDetialsDto> detialsDtos);
        Task<bool> DelCodeSchemeAsync(List<string> ids);
        Task<bool> UpdateCodeSchemeAsync(CodeGenSchemeDto codeSchemeDto, List<CodeSchemeDetialsDto> detialsDtos);
    }
}
