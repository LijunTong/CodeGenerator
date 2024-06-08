using CodeGenerator.Core.Dialog;
using CodeGenerator.Core.Event;
using CodeGenerator.Core.Mvvm;
using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Lib;
using CodeGenerator.Services.Interface;
using CommunityToolkit.Mvvm.Input;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.IO;
using System.Windows.Forms;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public partial class CodeHisViewModel : ListViewModelBase<CodeHisDto, CodeHis>
    {
        private ICodeHisSvc _CodeHisSvc;
        protected override string AddViewDialogName { get; set; } = "CodeHisEditView";
        protected override string EditViewDialogName { get; set; } = "CodeHisEditView";

        public CodeHisViewModel(ICodeHisSvc svc, IRegionManager regionManager, IDialogHostService dialogHostService, IEventAggregator eventAggregator) : base(dialogHostService, eventAggregator, regionManager, svc)
        {
            _CodeHisSvc = svc;
        }

        [RelayCommand]
        protected async Task ExportBtn()
        {
            try
            {
                var selectNum = ListItems.Where(x => x.IsSelected).ToList();
                if (!selectNum.Any())
                {
                    _eventAggregator.SendMessage("请至少选择一条数据");
                    return;
                }

                FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
                folderBrowserDialog.ShowNewFolderButton = true;
                var result = folderBrowserDialog.ShowDialog();
                if (result == System.Windows.Forms.DialogResult.OK)
                {
                    _eventAggregator.ToggleLoading(true);

                    await Task.Run(() =>
                    {
                        foreach (var item in selectNum)
                        {
                            string path = Path.Combine(Consts.SaveTempDir, item.FilePath);
                            FileInfo fileInfo = new FileInfo(path);
                            string desFilepath = Path.Combine(folderBrowserDialog.SelectedPath, fileInfo.Name);
                            fileInfo.CopyTo(desFilepath, true);
                        }
                    });
                    _eventAggregator.SendMessage("导出成功");
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex
                    , "ExportBtn");
                _eventAggregator.SendMessage("导出失败");
                return;
            }
            finally
            {
                _eventAggregator.ToggleLoading(false);
            }
        }

        protected override async Task<ButtonResult> DelBtn()
        {
            var result = await base.DelBtn();
            try
            {
                if (result != ButtonResult.OK)
                {
                    return result;
                }

                await _svc.DeleteByIdsAsync(_delIds.ToArray());
                _eventAggregator.SendMessage("删除成功");

                await GetListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }

            return result;
        }
    }
}
