using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Repository.Interface;
using CodeGenerator.Services.Interface;
using Jt.Common.Tool.DI;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CodeGenerator.Services
{
    public class CodeSchemeDetialsSvc : BaseSvc<CodeSchemeDetials>, ICodeSchemeDetialsSvc, ITransientDIInterface<ICodeSchemeDetialsSvc>
    {
        private ICodeSchemeDetialsRepository _detialsRepository;
        public CodeSchemeDetialsSvc(ICodeSchemeDetialsRepository repository) : base(repository)
        {
            _detialsRepository = repository;
        }

        public async Task<List<CodeSchemeDetialsDto>> GetDetialsBySchemeId(string schemeId)
        {
            return await _detialsRepository.GetCodeSchemeDetialsAsync(schemeId);
        }
    }
}
