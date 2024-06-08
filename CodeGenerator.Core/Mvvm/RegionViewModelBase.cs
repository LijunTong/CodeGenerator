using CommunityToolkit.Mvvm.ComponentModel;
using NLog;
using Prism.Regions;
using System;

namespace CodeGenerator.Core.Mvvm
{
    public partial class RegionViewModelBase : ViewModelBase, INavigationAware, IConfirmNavigationRequest
    {

        protected Logger _logger = LogManager.GetCurrentClassLogger();

        [ObservableProperty]
        private string _title;

        protected IRegionManager _regionManager { get; private set; }

        public RegionViewModelBase(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public virtual void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            continuationCallback(true);
        }

        public virtual bool IsNavigationTarget(NavigationContext navigationContext)
        {
            return true;
        }

        public virtual void OnNavigatedFrom(NavigationContext navigationContext)
        {

        }

        public virtual void OnNavigatedTo(NavigationContext navigationContext)
        {
            if (navigationContext.Parameters.ContainsKey("Title"))
            {
                Title = navigationContext.Parameters["Title"].ToString();
            }
        }
    }
}
