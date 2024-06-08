using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGenerator.Data.Entity
{
    [SugarTable("code_gen_scheme")]
    public class CodeGenScheme : BaseEntity
    {
        [SugarColumn(ColumnName = "name")]
        public string Name { get; set; }

        [SugarColumn(ColumnName = "des")]
        public string Des { get; set; }

    }
}