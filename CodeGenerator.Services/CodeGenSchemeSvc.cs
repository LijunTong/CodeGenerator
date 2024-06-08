using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Repository.Interface;
using CodeGenerator.Services.Interface;
using Jt.Common.Tool.DI;
using Jt.Common.Tool.Extension;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator.Services
{
    public class CodeGenSchemeSvc : BaseSvc<CodeGenScheme>, ICodeGenSchemeSvc, ITransientDIInterface<ICodeGenSchemeSvc>
    {
        private ICodeSchemeDetialsRepository _codeSchemeDetialsRepository;
        public CodeGenSchemeSvc(ICodeGenSchemeRepository repository, ICodeSchemeDetialsRepository codeSchemeDetialsRepository) : base(repository)
        {
            _codeSchemeDetialsRepository = codeSchemeDetialsRepository;
        }

        public async Task<bool> AddCodeSchemeAsync(CodeGenSchemeDto codeSchemeDto, List<CodeSchemeDetialsDto> detialsDtos)
        {
            try
            {
                CodeGenScheme entity = new CodeGenScheme
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = codeSchemeDto.Name,
                    Des = codeSchemeDto.Des,
                    IsDel = 0,
                    Creater = "sys",
                    CreateTime = DateTime.Now,
                    Updater = "sys",
                    UpTime = DateTime.Now,
                };

                List<CodeSchemeDetials> entities = new List<CodeSchemeDetials>();
                foreach (var item in detialsDtos)
                {
                    string fileName = item.FileName;
                    if (!item.FileName.Contains("."))
                    {
                        fileName = item.FileName + item.Suffix;
                    }
                    entities.Add(new CodeSchemeDetials
                    {
                        Id = Guid.NewGuid().ToString(),
                        FileName = fileName,
                        IsDel = 0,
                        Creater = "sys",
                        GenSchemeId = entity.Id,
                        TempId = item.TempId,
                        CreateTime = DateTime.Now,
                        Updater = "sys",
                        UpTime = DateTime.Now,
                    });
                }

                await _repository.InsertAsync(entity);
                await _codeSchemeDetialsRepository.InsertRangeAsync(entities);

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "AddCodeSchemeAsync");
                return false;
            }
        }

        public async Task<bool> DelCodeSchemeAsync(List<string> ids)
        {
            try
            {
                var delDetials = await _codeSchemeDetialsRepository.GetListAsync(x => ids.Contains(x.GenSchemeId));
                await _repository.DeleteByIdsAsync(ids.ToArray());
                await _codeSchemeDetialsRepository.DeleteByIdsAsync(delDetials.Select(x => x.Id).ToArray());
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "UpdateCodeSchemeAsync");
                return false;
            }
        }

        public async Task<bool> UpdateCodeSchemeAsync(CodeGenSchemeDto codeSchemeDto, List<CodeSchemeDetialsDto> detialsDtos)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(codeSchemeDto.Id);
                if (entity == null)
                {
                    return false;
                }

                entity.Name = codeSchemeDto.Name;
                entity.Des = codeSchemeDto.Des;
                entity.Updater = "sys";
                entity.UpTime = DateTime.Now;
                List<CodeSchemeDetials> entities = new List<CodeSchemeDetials>();
                foreach (var item in detialsDtos)
                {
                    string fileName = item.FileName;
                    if (Path.GetExtension(item.FileName).IsNullOrWhiteSpace())
                    {
                        fileName = item.FileName + item.Suffix;
                    }
                    entities.Add(new CodeSchemeDetials
                    {
                        Id = Guid.NewGuid().ToString(),
                        FileName = fileName,
                        IsDel = 0,
                        Creater = "sys",
                        GenSchemeId = entity.Id,
                        TempId = item.TempId,
                        CreateTime = DateTime.Now,
                        Updater = "sys",
                        UpTime = DateTime.Now,
                    });
                }
                var delDetials = await _codeSchemeDetialsRepository.GetListAsync(x => x.GenSchemeId == codeSchemeDto.Id);
                await _repository.UpdateAsync(entity);
                await _codeSchemeDetialsRepository.DeleteByIdsAsync(delDetials.Select(x => x.Id).ToArray());
                if (entities.Any())
                {
                    await _codeSchemeDetialsRepository.InsertRangeAsync(entities.ToArray());

                }
                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "UpdateCodeSchemeAsync");
                return false;
            }
        }
    }
}
