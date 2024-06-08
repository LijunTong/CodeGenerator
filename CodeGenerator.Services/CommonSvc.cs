using CodeGenerator.Data.Entity;
using CodeGenerator.Lib.Options;
using CodeGenerator.Services.Interface;
using Jt.Common.Tool.DI;
using Jt.Common.Tool.Helper;
using SqlSugar;
using System;
using System.IO;

namespace CodeGenerator.Services
{
    public class CommonSvc : ICommonSvc, ITransientDIInterface<ICommonSvc>
    {
        private readonly ISqlSugarClient _client;
        private readonly AppSetting _setting;

        public CommonSvc(ISqlSugarClient client, AppSetting appSetting)
        {
            _client = client;
            _setting = appSetting;
        }

        public void CodeFirstInitTables()
        {
            var dbDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "db");
            ResSystemHelper.Mkdirs(dbDir);
            var dbFile = Path.Combine(dbDir, "default.db");
            if (!File.Exists(dbFile))
            {
                File.Copy(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "default.db"), dbFile);
            }

            _client.CodeFirst.SetStringDefaultLength(200).InitTables(typeof(CodeDb), typeof(CodeGenScheme), typeof(CodeSchemeDetials), typeof(CodeTemp));
        }
    }
}
