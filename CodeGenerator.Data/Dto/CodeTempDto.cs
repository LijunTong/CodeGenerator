using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Data.Dto
{
    public partial class CodeTempDto : BaseDto
    {
        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string tempFilePath;

        [ObservableProperty]
        public string langType;

        [ObservableProperty]
        public string editContent;
    }
}
