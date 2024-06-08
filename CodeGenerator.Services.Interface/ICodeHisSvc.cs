using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using System.Threading.Tasks;

namespace CodeGenerator.Services.Interface
{
    public interface ICodeHisSvc : IBaseSvc<CodeHis>
    {
    	Task<bool> AddCodeHisAsync(CodeHisDto dto);

		Task<bool> UpdateCodeHisAsync(CodeHisDto dto);
    }
}
