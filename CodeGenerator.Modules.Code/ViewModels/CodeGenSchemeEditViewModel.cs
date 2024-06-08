using CodeGenerator.Core.Event;
using CodeGenerator.Core.Mvvm;
using CodeGenerator.Data.Dto;
using CodeGenerator.Lib;
using CodeGenerator.Services.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jt.Common.Tool.Extension;
using Prism.Events;
using Prism.Regions;
using System.Collections.ObjectModel;
using System.Windows;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public partial class CodeGenSchemeEditViewModel : RegionViewModelBase
    {
        private ICodeGenSchemeSvc _svc;
        private ICodeTempSvc _tempSvc;
        private IRegionNavigationJournal _journal;
        protected IEventAggregator _eventAggregator;
        private ICodeSchemeDetialsSvc _codeSchemeDetialsSvc;

        [ObservableProperty]
        private CodeGenSchemeDto _formData;
        [ObservableProperty]
        private ObservableCollection<CodeSchemeDetialsDto> _schemeDetialstos;

        public CodeGenSchemeEditViewModel(IRegionManager regionManager, ICodeGenSchemeSvc svc, ICodeTempSvc tempSvc, IEventAggregator eventAggregator, ICodeSchemeDetialsSvc codeSchemeDetialsSvc) : base(regionManager)
        {
            _svc = svc;
            _tempSvc = tempSvc;
            _eventAggregator = eventAggregator;
            _codeSchemeDetialsSvc = codeSchemeDetialsSvc;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);

            if (navigationContext.Parameters.ContainsKey("FormData"))
            {
                FormData = navigationContext.Parameters.GetValue<CodeGenSchemeDto>("FormData");
            }

            _journal = navigationContext.NavigationService.Journal;
            Laod();
        }

        private async void Laod()
        {
            try
            {
                SchemeDetialstos = new ObservableCollection<CodeSchemeDetialsDto>();
                // 获取temp
                var temps = await _tempSvc.GetListAsync(x => x.IsDel == 0);

                foreach (var temp in temps)
                {
                    Consts.LangFileSuffix.TryGetValue(temp.LangType, out var suffix);
                    SchemeDetialstos.Add(new CodeSchemeDetialsDto
                    {
                        TempId = temp.Id,
                        TempName = temp.Name,
                        Suffix = suffix
                    });
                }

                if (Title == "编辑")
                {
                    // 获取已选择的模板
                    var selected = await _codeSchemeDetialsSvc.GetListAsync(x => x.GenSchemeId == FormData.Id && x.IsDel == 0);
                    if (selected.IsNotNullOrEmpty())
                    {
                        foreach (var item in SchemeDetialstos)
                        {
                            var selectHit = selected.FirstOrDefault(x => x.TempId == item.TempId);
                            if (selectHit != null)
                            {
                                item.IsSelected = true;
                                item.FileName = selectHit.FileName;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
        }

        [RelayCommand]
        private async Task Save()
        {
            try
            {
                if (FormData.Name.IsNullOrWhiteSpace())
                {
                    return;
                }

                if (FormData.Des.IsNullOrWhiteSpace())
                {
                    return;
                }


                var selects = SchemeDetialstos.Where(x => x.IsSelected).ToList();

                if (!selects.Any())
                {
                    _eventAggregator.SendMessage("请选择模板");
                    return;
                }

                foreach (var select in selects)
                {
                    if (select.FileName.IsNullOrWhiteSpace())
                    {
                        _eventAggregator.SendMessage("选择模板的文件名不能为空");
                        return;
                    }
                }

                if (Title == "新增")
                {
                    bool isok = await _svc.AddCodeSchemeAsync(FormData, selects);
                    _eventAggregator.SendMessage(isok ? "新增成功" : "新增失败");
                }
                else if (Title == "编辑")
                {
                    bool isok = await _svc.UpdateCodeSchemeAsync(FormData, selects);
                    _eventAggregator.SendMessage(isok ? "编辑成功" : "编辑失败");
                }

                _journal.GoBack();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
        }

        [RelayCommand]
        private void Cancel()
        {
            _journal.GoBack();
        }

        [RelayCommand]
        private void Copy(object param)
        {
            if (param is string content && content.IsNotNullOrWhiteSpace())
            {
                Clipboard.SetText(content);
                _eventAggregator.SendMessage("复制成功");
            }
        }
    }
}
