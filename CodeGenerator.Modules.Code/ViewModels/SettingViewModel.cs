using CodeGenerator.Core.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public partial class SettingViewModel : RegionViewModelBase
    {
        [ObservableProperty]
        private ObservableCollection<string> _setMenu;

        [ObservableProperty]
        private Visibility _systemVisibility;

        [ObservableProperty]
        private Visibility _aboutVisibility;

        [ObservableProperty]
        private string _selectMenu;

        [ObservableProperty]
        private bool _isDrak;

        private readonly PaletteHelper _paletteHelper = new();


        public SettingViewModel(IRegionManager regionManager) : base(regionManager)
        {
            SetMenu = new ObservableCollection<string>()
            {
                "系统设置",
                "关于"
            };

            SelectionChanged("系统设置");

            Theme theme = _paletteHelper.GetTheme();
            var baseTheme = theme.GetBaseTheme();
            IsDrak = baseTheme == BaseTheme.Dark ? true : false;
        }

        [RelayCommand]
        private void SelectionChanged(string menu)
        {
            if (menu == "系统设置")
            {
                SystemVisibility = Visibility.Visible;
                AboutVisibility = Visibility.Hidden;
            }
            else if (menu == "关于")
            {
                SystemVisibility = Visibility.Hidden;
                AboutVisibility = Visibility.Visible;
            }
        }

        [RelayCommand]
        private void ThemeSwitch()
        {
            Theme theme = _paletteHelper.GetTheme();
            theme.SetBaseTheme(IsDrak ? BaseTheme.Dark : BaseTheme.Light);
            _paletteHelper.SetTheme(theme);
        }
    }
}
