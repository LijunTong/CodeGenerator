using CommunityToolkit.Mvvm.ComponentModel;

namespace CodeGenerator.Data.Dto
{
    public partial class CodeHisDto : BaseDto
    {
        [ObservableProperty]
    	public string name;
    	
        [ObservableProperty]
    	public string filePath;
    	
    }
}