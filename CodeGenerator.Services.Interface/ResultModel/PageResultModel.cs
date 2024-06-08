using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Services.Interface.ResultModel
{
    public class PageResultModel<T>
    {
        public List<T> List { get; set; }

        public long Total { get; set; }
    }
}
