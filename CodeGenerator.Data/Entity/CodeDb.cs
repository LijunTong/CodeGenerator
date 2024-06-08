using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGenerator.Data.Entity
{
    [SugarTable("code_db")]
    public class CodeDb : BaseEntity
    {
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        [SugarColumn(ColumnName = "type")]
        public string Type { get; set; }

        [SugarColumn(ColumnName = "con_str")]
        public string ConStr { get; set; }
    }
}