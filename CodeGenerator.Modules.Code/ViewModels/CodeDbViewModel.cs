using CodeGenerator.Core.Dialog;
using CodeGenerator.Core.Event;
using CodeGenerator.Core.Mvvm;
using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Services.Interface;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public partial class CodeDbViewModel : ListViewModelBase<CodeDbDto, CodeDb>
    {
        private ICodeDbSvc _codeDbSvc;
        protected override string AddViewDialogName { get; set; } = "CodeDbEditView";
        protected override string EditViewDialogName { get; set; } = "CodeDbEditView";

        public CodeDbViewModel(ICodeDbSvc codeDbSvc, IRegionManager regionManager, IDialogHostService dialogHostService, IEventAggregator eventAggregator) : base(dialogHostService, eventAggregator, regionManager, codeDbSvc)
        {
            _codeDbSvc = codeDbSvc;
        }

        protected override async Task<ButtonResult> InsertBtn()
        {
            var result = await base.InsertBtn();
            try
            {
                if (result != ButtonResult.OK)
                {
                    return result;
                }

                bool isok = await _codeDbSvc.AddCodeDbAsync(Item);
                _eventAggregator.SendMessage(isok ? "新增成功" : "新增失败");
                await GetListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
            return result;
        }

        protected override async Task<ButtonResult> EditBtn()
        {
            var result = await base.EditBtn();
            try
            {
                if (result != ButtonResult.OK)
                {
                    return result;
                }

                bool isok = await _codeDbSvc.UpdateCodeDbAsync(Item);
                _eventAggregator.SendMessage(isok ? "编辑成功" : "编辑失败");

                await GetListAsync();
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }

            return result;
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

                await _codeDbSvc.DeleteByIdsAsync(_delIds.ToArray());
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
