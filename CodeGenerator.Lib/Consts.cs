using CodeGenerator.Lib.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Lib
{
    public static class Consts
    {
        public const string ContentRegion = "ContentRegion";

        public static readonly Dictionary<string, string> LangFileSuffix = new Dictionary<string, string>()
        {
            { "Java", ".java" },
            { "C", ".c" },
            { "C++", ".cpp" },
            { "C#", ".cs" },
            { "Python", ".py" },
            { "JavaScript", ".js" },
            { "PHP", ".php" },
            { "Ruby", ".rb" },
            { "Swift", ".swift" },
            { "Objective-C", ".m" },
            { "Kotlin", ".kt" },
            { "Go", ".go" },
            { "Rust", ".rs" },
            { "TypeScript", ".ts" },
            { "Scala", ".scala" },
            { "Perl", ".pl" },
            { "R", ".r" },
            { "MATLAB", ".m" },
            { "Shell", ".sh" },
            { "HTML", ".html" },
            { "CSS", ".css" },
            { "SQL", ".sql" },
            { "Batch", ".bat" },
            { "Powershell", ".ps1" },
            { "Racket", ".rkt" },
            { "Haskell", ".hs" },
            { "Lua", ".lua" },
            { "Dart", ".dart" },
            { "Groovy", ".groovy" },
            { "Julia", ".jl" },
            { "SAS", ".sas" },
            { "Visual Basic", ".vb" },
            { "COBOL", ".cob" },
            { "Fortran", ".f" },
            { "Ada", ".ada" },
            { "Prolog", ".pl" },
            { "Lisp", ".lisp" },
            { "Scheme", ".scm" },
            { "Verilog", ".v" },
            { "VHDL", ".vhdl" },
            { "Assembly", ".asm" },
            { "XmlDoc", ".xml" },
            { "ASP/XHTML", ".asp" },
            { "Boo", ".boo" },
            { "Coco", ".atg" },  // 这个比较少见，通常是用来描述语法的定义
            { "Patch", ".patch" },
            { "TeX", ".tex" },
            { "TSQL", ".sql" },
            { "VB", ".vb" },
            { "Markdown", ".md" },
            { "MarkdownWithFontSize", ".md" },  // 这个通常是自定义扩展，文件后缀与普通 Markdown 相同
            { "Json", ".json" }
        };

        public static readonly string TempDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates");
        public static readonly string SaveTempDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Gen");

        public static readonly List<Placeholder> Placeholders = new List<Placeholder>
        {
            new Placeholder
            {
                Name = "@(Model.TableName)",
                Des = "字符串类型，数据库表名称"
            },
            new Placeholder
            {
                Name = "@(Model.ClassName)",
                Des = "字符串类型，实体类名称"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos)",
                Des = "数据类型，数据库表结构，字段列表，可用遍历获取字段信息"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].FieldName)",
                Des = "字符串类型，数据库字段名称-下划线命名"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].FieldModelName)",
                Des = "字符串类型，实体类属性名称-Pascal命名"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].FieldModelNameCamel)",
                Des = "字符串类型，实体类属性名称-驼峰命名"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].FieldDbType)",
                Des = "字符串类型，数据库字段类型"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].FieldModelType)",
                Des = "字符串类型，实体类属性类型"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].FieldLength)",
                Des = "数值类型，数据库字符串类型字段长度"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].IsNotNull)",
                Des = "字符串类型，数据库字段是否允许为空，YES：允许为空，NO：不能为空"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].IsIncrement)",
                Des = "数值类型，数据库数值类型是否自增，1：自增，0：不自增"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].IsPrimaryKey)",
                Des = "数值类型，数据库数值类型是否主键，1：主键，0：非主键"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].FieldDes)",
                Des = "字符串类型，数据库字段描述"
            },
            new Placeholder
            {
                Name = "@(Model.DbFieldInfos[i].DefaultValue)",
                Des = "字符串类型，数据库字段默认值"
            }
        };

        public static readonly Dictionary<string, string> DicMysqlFieldMap = new Dictionary<string, string>()
            {
                {"int","Int32"},
                {"bigint","Int64"},
                {"binary","Byte[]"},
                {"bit","Boolean"},
                {"char","string"},
                {"nchar","string"},
                 {"varchar","string"},
                 {"text","string"},
                {"nvarchar","string"},
                {"datetime","DateTime"},
                {"decimal","Decimal"},
                {"float","Double"},
                {"image","Byte[]"},
                {"money","Decimal"},
                {"ntext","string"},
                {"real","Single"},
                {"smalldatetime","DateTime"},
                {"smallint","Int16"},
                {"smallmoney","Decimal"},
                {"timestamp","DateTime"},
                {"tinyint","Byte"},
                {"uniqueidentifier","Guid"},
                {"varbinary","Byte[]"},
                {"Variant","Object"}
            };
        public static readonly Dictionary<string, string> DicSqlServerFieldMap = new Dictionary<string, string>()
        {
            {"int","Int32"},
            {"text","string"},
            {"bigint","Int64"},
            {"binary","byte[]"},
            {"bit","bool"},
            {"char","string"},
            {"datetime","DateTime"},
            {"decimal","decimal"},
            {"float","double"},
            {"image","byte[]"},
            {"money","Decimal"},
            {"nchar","string"},
            {"ntext","string"},
            {"numeric","Decimal"},
            {"nvarchar","string"},
            {"real","Single"},
            {"smalldatetime","DateTime"},
            {"smallint","Int16"},
            {"smallmoney","Decimal"},
            {"timestamp","DateTime"},
            {"tinyint","byte"},
            {"uniqueidentifier","Guid"},
            {"varbinary","byte[]"},
            {"varchar","string"},
            {"Variant","object"},
        };
        public static readonly Dictionary<string, string> DicPostgreSQLFieldMap = new Dictionary<string, string>()
        {
             {"bool","bool"},
            {"int2","short"},
            {"int4","int"},
            {"int8","long"},
            {"float4","float"},
            {"float8","double"},
            {"numeric","decimal"},
            {"money","decimal"},
            {"text","string"},
            {"varchar","string"},
            {"bpchar","string"},
            {"citext","string"},
            {"json","string"},
            {"jsonb","string"},
            {"xml","string"},
            {"point","NpgsqlPoint"},
            {"lseg","NpgsqlLSeg"},
            {"path","NpgsqlPath"},
            {"polygon","NpgsqlPolygon"},
            {"line","NpgsqlLine"},
            {"circle","NpgsqlCircle"},
            {"box","NpgsqlBox"},
            {"bit(1)","bool"},
            {"bit(n)","BitArray"},
            {"varbit","BitArray"},
            {"hstore","IDictionary<string, string>"},
            {"uuid","Guid"},
            {"cidr","NpgsqlInet"},
            {"inet","IPAddress"},
            {"macaddr","PhysicalAddress"},
            {"tsquery","NpgsqlTsQuery"},
            {"tsvector","NpgsqlTsVector"},
            {"date","DateTime"},
            {"interval","TimeSpan"},
            {"timestamp","DateTime"},
            {"timestamptz","DateTime"},
            {"time","TimeSpan"},
            {"timetz","DateTimeOffset"},
            {"bytea","byte[]"},
            {"oid","uint"},
            {"xid","uint"},
            {"cid","uint"},
            {"oidvector","uint[]"},
            {"name","string"},
            {"(internal) char","char"},
            {"geometry (PostGIS)","PostgisGeometry"},
            {"record","object[]"},
            {"composite types","T"},
            {"range subtypes","NpgsqlRange"},
            {"enum types","TEnum"},
            {"array types","Array"},
        };

    }



}
