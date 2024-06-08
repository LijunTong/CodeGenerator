using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Data.Dto
{
    public class CodeTempParamsDto
    {
        public string CodeSchemeId { get; set; }
        public string CodeSchemeName { get; set; }
        public string TableName { get; set; }
        public string ClassName { get; set; }
        public List<DbFieldInfoDto> DbFieldInfos { get; set; }
        public List<CodeSchemeDetialsDto> Temps { get; set; }
    }
}
