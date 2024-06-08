using CodeGenerator.Modules.Code.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace CodeGenerator.Modules.ModuleName
{
    public class ModuleCodeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public ModuleCodeModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void OnInitialized(IContainerProvider containerProvider)
        {
        }

        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // RegisterForNavigation
            containerRegistry.RegisterForNavigation<HomeView>();
            containerRegistry.RegisterForNavigation<CodeDbView>();
            containerRegistry.RegisterForNavigation<CodeTempView>();
            containerRegistry.RegisterForNavigation<CodeGenSchemeView>();
            containerRegistry.RegisterForNavigation<GenView>();
            containerRegistry.RegisterForNavigation<SettingView>();
            containerRegistry.RegisterForNavigation<CodeHisView>();

            // RegisterDialog
            containerRegistry.RegisterForNavigation<CodeDbEditView>();
            containerRegistry.RegisterForNavigation<CodeTempEditView>();
            containerRegistry.RegisterForNavigation<CodeGenSchemeEditView>();
        }
    }
}