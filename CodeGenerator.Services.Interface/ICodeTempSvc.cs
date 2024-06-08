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
    public interface ICodeTempSvc : IBaseSvc<CodeTemp>
    {
        Task<bool> AddCodeTempAsync(CodeTempDto codeTempDto);

        Task<bool> UpdateCodeTempAsync(CodeTempDto codeTempDto);

        Task<bool> DeleteCodeTempAsync(CodeTempDto[] codeTemps);

        Task<List<CodeTemp>> GetCodeTempsByIdsAsync(string[] ids);
    }
}
