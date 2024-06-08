using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Data.Dto
{
    public partial class CodeSchemeDetialsDto : BaseDto
    {
        [ObservableProperty]
        public string fileName;

        [ObservableProperty]
        public string tempId;

        [ObservableProperty]
        public string genSchemeId;

        [ObservableProperty]
        public string tempName;

        [ObservableProperty]
        public string suffix;
    }
}
