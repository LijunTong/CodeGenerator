using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using System.Threading.Tasks;

namespace CodeGenerator.Services.Interface
{
    public interface ICodeDbSvc : IBaseSvc<CodeDb>
    {
        Task<bool> AddCodeDbAsync(CodeDbDto codeDbDto);

        Task<bool> UpdateCodeDbAsync(CodeDbDto codeDbDto);
    }
}
