using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Repository.Interface;
using CodeGenerator.Services.Interface;
using Jt.Common.Tool.DI;
using System;
using System.Threading.Tasks;

namespace CodeGenerator.Services
{
    public class CodeDbSvc : BaseSvc<CodeDb>, ICodeDbSvc, ITransientDIInterface<ICodeDbSvc>
    {
        public CodeDbSvc(ICodeDbRepository repository) : base(repository)
        {
        }

        public async Task<bool> AddCodeDbAsync(CodeDbDto codeDbDto)
        {
            try
            {
                CodeDb entity = new CodeDb()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = codeDbDto.Name,
                    ConStr = codeDbDto.ConStr,
                    Type = codeDbDto.Type,
                    IsDel = 0,
                    Creater = "sys",
                    CreateTime = DateTime.Now,
                    Updater = "sys",
                    UpTime = DateTime.Now,
                };
                return await _repository.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "AddCodeDbAsyncs");
                return false;
            }
        }

        public async Task<bool> UpdateCodeDbAsync(CodeDbDto codeDbDto)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(codeDbDto.Id);
                if (entity == null)
                {
                    _logger.Error($"UpdateCodeDbAsync：实体不存在，id={codeDbDto.Id}");
                    return false;
                }

                entity.Name = codeDbDto.Name;
                entity.Type = codeDbDto.Type;
                entity.ConStr = codeDbDto.ConStr;
                entity.Updater = "sys";
                entity.UpTime = DateTime.Now;

                return await _repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "UpdateCodeDbAsync");
                return false;
            }
        }
    }
}
