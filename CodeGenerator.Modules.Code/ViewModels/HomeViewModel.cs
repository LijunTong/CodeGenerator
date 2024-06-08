using CodeGenerator.Core.Mvvm;
using Prism.Regions;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public partial class HomeViewModel : RegionViewModelBase
    {

        public HomeViewModel(IRegionManager regionManager) : base(regionManager)
        {
        }
    }
}
