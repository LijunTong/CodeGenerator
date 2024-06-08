using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Data.Entity.DatabaseEntity;
using CodeGenerator.Lib;
using CodeGenerator.Repository.DbExtracts;
using CodeGenerator.Repository.Interface;
using CodeGenerator.Services.Interface;
using Jt.Common.Tool.Helper;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DbTableInfo = CodeGenerator.Data.Entity.DatabaseEntity.DbTableInfo;

namespace CodeGenerator.Services
{
    public class CodeGeneratorSvc : ICodeGeneratorSvc
    {
        private IDbExtractRepo _repo;

        public CodeGeneratorSvc(DbType dbType, string conStr)
        {
            _repo = Factory.GetDbExtractRepo(dbType, conStr);
        }


        public async Task<(string RelativePath, string AbsolutePath)> CodeGenerateAsync(List<CodeTemp> temps, CodeTempParamsDto codeTempParams)
        {
            string fileName = $"{codeTempParams.TableName}表_{codeTempParams.CodeSchemeName}方案_{DateTime.Now.ToString("yyyyMMddHHmmss")}";
            string fileDir = Path.Combine(Consts.SaveTempDir, fileName);
            ResSystemHelper.Mkdirs(fileDir);
            foreach (var item in codeTempParams.Temps)
            {
                var temp = temps.FirstOrDefault(x => x.Id == item.TempId);
                if (temp == null)
                {
                    throw new Exception("未找到模板");
                }

                string key = Guid.NewGuid().ToString();
                try
                {
                    await Task.Run(() =>
                    {
                        string tempFilePath = Path.Combine(Consts.TempDir, temp.TempFilePath);
                        string template = File.ReadAllText(tempFilePath);
                        string content = Utils.CodeGenerateRazorEngine(key, template, codeTempParams);
                        FileInfoHelper.WriteToFile(fileDir, item.FileName, content);
                    });
                }
                catch (Exception ex)
                {
                    string msg = ex.Message.ToString();
                    FileInfoHelper.WriteToFile(fileDir, item.FileName + ".log", msg);
                }
            }

            string data = "";
            if (ZipHelper.Zip(fileDir))
            {
                data = fileDir + ".zip";
            }

            return (fileName, data);
        }

        public async Task<List<DbInfo>> GetDataBasesAsync()
        {
            return await _repo.GetDataBasesAsync();
        }

        public async Task<List<DbFieldInfo>> GetDbFieldsAsync(string dbName, string tableName)
        {
            return await _repo.GetDbFieldsAsync(dbName, tableName);
        }

        public async Task<List<DbTableInfo>> GetTableNamesAsync(string dbName)
        {
            return await _repo.GetTableNamesAsync(dbName);
        }
    }
}
