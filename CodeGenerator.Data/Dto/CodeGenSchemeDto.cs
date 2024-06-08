using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Data.Dto
{
    public partial class CodeGenSchemeDto : BaseDto
    {
        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string des;
    }
}
