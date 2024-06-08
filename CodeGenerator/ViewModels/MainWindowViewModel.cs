using CodeGenerator.Core.Mvvm;
using CodeGenerator.Dto;
using CodeGenerator.Lib;
using CodeGenerator.Lib.Options;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jt.Common.Tool.Extension;
using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace CodeGenerator.ViewModels
{
    public partial class MainWindowViewModel : RegionViewModelBase
    {
        private readonly AppSetting _appSetting;
        private IRegionNavigationJournal _navigationJournal;

        [ObservableProperty]
        private string _title;

        [ObservableProperty]
        private ObservableCollection<MenuModel> _menu;

        [ObservableProperty]
        private bool _isLeftDrawerOpen = true;

        [ObservableProperty]
        private MenuModel _selectedMenu;

        [ObservableProperty]
        private bool _canGoBack = false;

        [ObservableProperty]
        private bool _canGoForward = false;

        [ObservableProperty]
        private string _version;

        [ObservableProperty]
        private string maxIconKind = "WindowMaximize";

        public MainWindowViewModel(AppSetting appSetting, MenuOptions menuOptions, IRegionManager regionManager) : base(regionManager)
        {
            this._appSetting = appSetting;
            Title = appSetting.Title;
            Menu = menuOptions.Menus.ToJson().ToObj<ObservableCollection<MenuModel>>();
        }

        [RelayCommand]
        private void SelectionChanged(MenuModel menu)
        {
            if (menu != SelectedMenu)
            {
                return;
            }

            GoTo(menu);
            SetPrevNextBtnEnable();
        }

        [RelayCommand]
        private void MovePrev()
        {
            _navigationJournal.GoBack();
            // 因为上面SelectionChanged的参数有title，所以能获取
            var currentHeader = _navigationJournal.CurrentEntry.Parameters.GetValue<string>("Title");
            SetPrevNextBtnEnable();
            SelectedListBoxItem(currentHeader);
        }

        [RelayCommand]
        private void MoveNext()
        {
            _navigationJournal.GoForward();
            SetPrevNextBtnEnable();
            var currentHeader = _navigationJournal.CurrentEntry.Parameters.GetValue<string>("Title");
            SelectedListBoxItem(currentHeader);
        }

        [RelayCommand]
        private void Home()
        {
            var home = Menu.FirstOrDefault(x => x.Header == "首页");
            GoTo(home);
            SetPrevNextBtnEnable();
            SelectedListBoxItem("首页");
        }

        [RelayCommand]
        private void Setting()
        {
            NavigationParameters keyValues = new NavigationParameters() { { "Title", "设置" } };
            _regionManager.RequestNavigate(Consts.ContentRegion, "SettingView", back =>
            {
                _navigationJournal = back.Context?.NavigationService?.Journal;
            }, keyValues);
            SetPrevNextBtnEnable();
            SelectedListBoxItem("");
        }

        private MenuModel GetTreeSelected(List<MenuModel> root)
        {
            if (root == null)
            {
                return null;
            }

            foreach (var item in root)
            {
                if (item.IsSelected)
                {
                    return item;
                }

                var subMenu = item.SubMenu?.ToList();
                var result = GetTreeSelected(subMenu);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private MenuModel GetTreeSelected(List<MenuModel> root, string header)
        {
            if (root == null)
            {
                return null;
            }

            foreach (var item in root)
            {
                if (item.Header == header)
                {
                    return item;
                }

                var subMenu = item.SubMenu?.ToList();
                var result = GetTreeSelected(subMenu, header);
                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        private void SetPrevNextBtnEnable()
        {
            CanGoBack = _navigationJournal.CanGoBack;
            CanGoForward = _navigationJournal.CanGoForward;
        }

        private void GoTo(MenuModel menu)
        {
            var currentHeader = _navigationJournal?.CurrentEntry.Parameters.GetValue<string>("Title");
            if (menu.Header == currentHeader)
            {
                return;
            }

            NavigationParameters keyValues = new NavigationParameters() { { "Title", menu.Header } };
            _regionManager.RequestNavigate(Consts.ContentRegion, menu.View, back =>
            {
                _navigationJournal = back.Context?.NavigationService?.Journal;
            }, keyValues);
        }

        private void SelectedListBoxItem(string header)
        {
            var select = GetTreeSelected(Menu.ToList(), header);
            if (SelectedMenu != select)
            {
                SelectedMenu = select;
            }
        }

        [RelayCommand]
        private void Load()
        {
            // 获取当前程序集
            Assembly assembly = Assembly.GetExecutingAssembly();

            // 获取版本信息
            Version version = assembly.GetName().Version;
            this.Version = "V " + version.ToString();
            Home();
        }
    }
}
