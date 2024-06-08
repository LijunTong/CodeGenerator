using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeGenerator.Data.Dto
{
    public partial class CodeDbDto : BaseDto
    {
        [ObservableProperty]
        public string name;

        [ObservableProperty]
        public string type;

        [ObservableProperty]
        public string conStr;
    }
}
