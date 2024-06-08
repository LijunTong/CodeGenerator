using CodeGenerator.Core.Event;
using CodeGenerator.Core.Mvvm;
using CodeGenerator.Data.Dto;
using CodeGenerator.Lib;
using CodeGenerator.Lib.Models;
using CodeGenerator.Services.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ICSharpCode.AvalonEdit;
using Jt.Common.Tool.Extension;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public partial class CodeTempEditViewModel : RegionViewModelBase
    {
        private IEventAggregator _eventAggregator;

        [ObservableProperty]
        private CodeTempDto _formData;

        [ObservableProperty]
        private ObservableCollection<string> _highLight;

        private IRegionNavigationJournal _navigationJournal;

        private ICodeTempSvc _codeTempSvc;

        [ObservableProperty]
        private List<Placeholder> _placeholders;

        public TextEditor TextEditor { get; set; }

        public CodeTempEditViewModel(IRegionManager regionManager, ICodeTempSvc codeTempSvc, IEventAggregator eventAggregator) : base(regionManager)
        {
            _highLight = new ObservableCollection<string>();
            // var allHighlight = HighlightingManager.Instance.HighlightingDefinitions.Select(x => x.Name).ToArray();
            foreach (var item in Consts.LangFileSuffix.Keys)
            {
                _highLight.Add(item);
            }
            _codeTempSvc = codeTempSvc;
            _placeholders = Consts.Placeholders;
            _eventAggregator = eventAggregator;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            if (navigationContext.Parameters.ContainsKey("FormData"))
            {
                FormData = navigationContext.Parameters.GetValue<CodeTempDto>("FormData");
            }

            _navigationJournal = navigationContext.NavigationService.Journal;
        }

        [RelayCommand]
        public async Task SaveAsync()
        {
            try
            {
                if (FormData.Name.IsNullOrWhiteSpace())
                {
                    return;
                }

                if (FormData.LangType.IsNullOrWhiteSpace())
                {
                    return;
                }

                if (FormData.EditContent.IsNullOrWhiteSpace())
                {
                    return;
                }

                if (Title == "新增")
                {
                    bool isok = await _codeTempSvc.AddCodeTempAsync(FormData);
                    _eventAggregator.SendMessage(isok ? "新增成功" : "新增失败");

                }
                else if (Title == "编辑")
                {
                    bool isok = await _codeTempSvc.UpdateCodeTempAsync(FormData);
                    _eventAggregator.SendMessage(isok ? "编辑成功" : "编辑失败");
                }

                _navigationJournal.GoBack();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
        }

        [RelayCommand]
        public void Cancel()
        {
            _navigationJournal.GoBack();
        }

        [RelayCommand]
        public void Paste()
        {
            TextEditor.Paste();
        }

        [RelayCommand]
        public void PasteText(object parameter)
        {
            Clipboard.SetText(parameter.ToString());
            TextEditor.Paste();
        }
    }
}
