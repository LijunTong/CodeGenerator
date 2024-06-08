using CodeGenerator.Data.Entity.DatabaseEntity;
using CodeGenerator.Data.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeGenerator.Data.Dto;

namespace CodeGenerator.Services.Interface
{
    public interface ICodeGeneratorSvc
    {
        Task<List<DbInfo>> GetDataBasesAsync();

        Task<List<DbTableInfo>> GetTableNamesAsync(string dbName);

        Task<List<DbFieldInfo>> GetDbFieldsAsync(string dbName, string tableName);

        Task<(string RelativePath,string AbsolutePath)> CodeGenerateAsync(List<CodeTemp> temps, CodeTempParamsDto codeTempParams);
    }
}
