using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Lib;
using CodeGenerator.Repository.Interface;
using CodeGenerator.Services.Interface;
using Jt.Common.Tool.DI;
using Jt.Common.Tool.Extension;
using Jt.Common.Tool.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CodeGenerator.Services
{
    public class CodeTempSvc : BaseSvc<CodeTemp>, ICodeTempSvc, ITransientDIInterface<ICodeTempSvc>
    {
        public CodeTempSvc(ICodeTempRepository repository) : base(repository)
        {
        }

        public async Task<bool> AddCodeTempAsync(CodeTempDto codeTempDto)
        {
            try
            {
                CodeTemp entity = new CodeTemp()
                {
                    Id = Guid.NewGuid().ToString(),
                    Name = codeTempDto.Name,
                    LangType = codeTempDto.LangType,
                    TempFilePath = codeTempDto.TempFilePath,
                    IsDel = 0,
                    Creater = "sys",
                    CreateTime = DateTime.Now,
                    Updater = "sys",
                    UpTime = DateTime.Now,
                };
                string suffix = Consts.LangFileSuffix.TryGetValue(entity.LangType, out var suffixString) ? suffixString : "";
                entity.TempFilePath = entity.Id + suffix;
                // 写入文件
                FileInfoHelper.WriteToFile(Consts.TempDir, entity.TempFilePath, codeTempDto.EditContent);

                return await _repository.InsertAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "AddCodeTempAsync");
                return false;
            }
        }

        public async Task<bool> DeleteCodeTempAsync(CodeTempDto[] codeTemps)
        {
            try
            {
                var selectIds = codeTemps.Select(x => x.Id).ToArray();
                await _repository.DeleteByIdsAsync(selectIds);
                foreach (var item in codeTemps)
                {
                    if (item.TempFilePath.IsNotNullOrWhiteSpace())
                    {
                        File.Delete(Path.Combine(Consts.TempDir, item.TempFilePath));
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "DeleteCodeTempAsync");
                return false;
            }
        }

        public async Task<List<CodeTemp>> GetCodeTempsByIdsAsync(string[] ids)
        {
            return await _repository.GetListAsync(x => ids.Contains(x.Id));
        }

        public async Task<bool> UpdateCodeTempAsync(CodeTempDto codeTempDto)
        {
            try
            {
                CodeTemp entity = _repository.GetById(codeTempDto.Id);
                if (entity == null)
                {
                    return false;
                }


                entity.Name = codeTempDto.Name;
                entity.LangType = codeTempDto.LangType;
                entity.Updater = "sys";
                entity.UpTime = DateTime.Now;


                string suffix = Consts.LangFileSuffix.TryGetValue(codeTempDto.LangType, out var suffixString) ? suffixString : "";
                entity.TempFilePath = entity.Id + suffix;
                FileInfoHelper.WriteToFile(Consts.TempDir, entity.TempFilePath, codeTempDto.EditContent);

                return await _repository.UpdateAsync(entity);
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "AddCodeDbAsyncs");
                return false;
            }
        }
    }
}
