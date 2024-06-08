using CodeGenerator.Core.Dialog;
using CodeGenerator.Core.Event;
using CodeGenerator.Core.Mvvm;
using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Lib;
using CodeGenerator.Services.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jt.Common.Tool.Extension;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Diagnostics;
using System.IO;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public partial class CodeTempViewModel : ListViewModelBase<CodeTempDto, CodeTemp>
    {
        private ICodeTempSvc _codeTempSvc;
        [ObservableProperty]
        private CodeTempDto _codeTempDto;

        public CodeTempViewModel(IEventAggregator eventAggregator, IDialogHostService dialogHostService, IRegionManager regionManager, ICodeTempSvc codeTempSvc) : base(dialogHostService, eventAggregator, regionManager, codeTempSvc)
        {
            _codeTempSvc = codeTempSvc;
        }

        protected override string AddViewDialogName { get; set; } = "CodeTempEditView";
        protected override string EditViewDialogName { get; set; } = "CodeTempEditView";

        protected override async Task<ButtonResult> InsertBtn()
        {
            try
            {
                Item = new CodeTempDto();

                NavigationParameters navigationParameters = new NavigationParameters()
            {
                { "Title", "新增" },
                {"FormData", Item }
            };

                _regionManager.RequestNavigate(Consts.ContentRegion, AddViewDialogName, navigationParameters);
                await Task.CompletedTask;

                return ButtonResult.OK;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
                return ButtonResult.Cancel;
            }
        }

        protected override async Task<ButtonResult> EditBtn()
        {
            try
            {
                if (ListItems.Count(x => x.IsSelected) != 1)
                {
                    _eventAggregator.SendMessage("请选择一条数据");
                    return ButtonResult.Cancel;
                }

                Item = ListItems.FirstOrDefault(x => x.IsSelected).DeepCopyBySerialize();
                string filePath = Path.Combine(Consts.TempDir, Item.TempFilePath);
                if (File.Exists(filePath))
                {
                    Item.EditContent = File.ReadAllText(filePath);
                }

                NavigationParameters navigationParameters = new NavigationParameters()
            {
                { "Title", "编辑" },
                {"FormData", Item }
            };

                _regionManager.RequestNavigate(Consts.ContentRegion, AddViewDialogName, navigationParameters);
                await Task.CompletedTask;

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
            return ButtonResult.OK;
        }

        protected override async Task<ButtonResult> DelBtn()
        {
            try
            {
                var selects = ListItems.Where(x => x.IsSelected).ToArray();
                var selectNum = selects.Select(x => x.Id);
                if (!selectNum.Any())
                {
                    _eventAggregator.SendMessage("请至少选择一条数据");
                    return ButtonResult.Cancel;
                }

                var result = await _dialogHostService.ShowComfirmDialog($"确认删除 {selectNum.Count()} 条记录吗？删除后将无法恢复！");
                if (result.Result == ButtonResult.OK)
                {
                    bool isok = await _codeTempSvc.DeleteCodeTempAsync(selects);
                    _eventAggregator.SendMessage(isok ? "删除成功" : "删除失败");
                    await GetListAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
            return ButtonResult.OK;
        }

        [RelayCommand]
        private void OpenInApp(object param)
        {
            try
            {
                if (CodeTempDto != null)
                {
                    string filePath = Path.Combine(Consts.TempDir, CodeTempDto.TempFilePath);
                    Process.Start(new ProcessStartInfo(filePath) { UseShellExecute = true });
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
        }
    }
}
