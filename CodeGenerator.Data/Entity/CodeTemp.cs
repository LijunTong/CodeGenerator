using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGenerator.Data.Entity
{
    [SugarTable("code_temp")]
    public class CodeTemp : BaseEntity
    {
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        [SugarColumn(ColumnName = "temp_file_path")]
        public string TempFilePath { get; set; }

        [SugarColumn(ColumnName = "lang_type")]
        public string LangType { get; set; }
    }
}