using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGenerator.Data.Entity
{
    [SugarTable("code_scheme_detials")]
    public class CodeSchemeDetials : BaseEntity
    {
        [SugarColumn(ColumnName = "file_name")]
        public string FileName { get; set; }

        [SugarColumn(ColumnName = "temp_id")]
        public string TempId { get; set; }

        [SugarColumn(ColumnName = "gen_scheme_id")]
        public string GenSchemeId { get; set; }


    }
}