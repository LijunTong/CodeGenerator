using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGenerator.Data.Entity
{
    public class BaseEntity
    {
        [SugarColumn(ColumnName = "id", IsPrimaryKey = true)]
        public string Id { get; set; }

        [SugarColumn(ColumnName = "creater")]
        public string Creater { get; set; }

        [SugarColumn(ColumnName = "create_time")]
        public DateTime CreateTime { get; set; }

        [SugarColumn(ColumnName = "updater")]
        public string Updater { get; set; }

        [SugarColumn(ColumnName = "up_time")]
        public DateTime UpTime { get; set; }

        [SugarColumn(ColumnName = "is_del")]
        public int IsDel { get; set; }

    }
}
