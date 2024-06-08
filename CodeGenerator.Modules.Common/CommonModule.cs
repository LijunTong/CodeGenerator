
using CodeGenerator.Modules.Common.ViewModels;
using CodeGenerator.Modules.Common.Views;
using Prism.Ioc;
using Prism.Modularity;

namespace CodeGenerator.Modules.Common
{
    public class CommonModule : IModule
    {
        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<ComComfirmDialogView, ComComfirmDialogViewModel>();
            containerRegistry.RegisterForNavigation<LoadingView>();

        }
    }

}
