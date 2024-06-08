using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Repository.Interface;
using CodeGenerator.Services.Interface;
using System;
using System.Threading.Tasks;

namespace CodeGenerator.Services
{
    public class CodeHisSvc : BaseSvc<CodeHis>, ICodeHisSvc
    {
        public CodeHisSvc(ICodeHisRepository repository) : base(repository)
        {
        }
        
    	public async Task<bool> AddCodeHisAsync(CodeHisDto dto)
		{
		    try
		    {
		        CodeHis entity = new CodeHis()
		        {
		        	Name = dto.Name,
					FilePath = dto.FilePath,
		            Id = Guid.NewGuid().ToString(),
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
		        _logger.Error(ex, "AddCodeHisAsyncs");
		        return false;
		    }
		}

		public async Task<bool> UpdateCodeHisAsync(CodeHisDto dto)
		{
		    try
		    {
		        var entity = await _repository.GetByIdAsync(dto.Id);
		        if (entity == null)
		        {
		            _logger.Error($"UpdateCodeHisAsync：实体不存在，id={dto.Id}");
		            return false;
		        }
		
				entity.Name = dto.Name;
				entity.FilePath = dto.FilePath;
		        entity.Updater = "sys";
		        entity.UpTime = DateTime.Now;
		
		        return await _repository.UpdateAsync(entity);
		    }
		    catch (Exception ex)
		    {
		        _logger.Error(ex, "UpdateCodeHisAsync");
		        return false;
		    }
		}	
    
    }
}