using SqlSugar;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeGenerator.Data.Entity
{
   [SugarTable("code_his")]
    public class CodeHis : BaseEntity
    {
        	[SugarColumn(ColumnName = "name")]
        	public string Name { get; set; }
        	
        	[SugarColumn(ColumnName = "file_path")]
        	public string FilePath { get; set; }
        	
    }
}