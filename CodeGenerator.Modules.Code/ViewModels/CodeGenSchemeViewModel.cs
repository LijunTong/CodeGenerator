using CodeGenerator.Core.Dialog;
using CodeGenerator.Core.Event;
using CodeGenerator.Core.Mvvm;
using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Lib;
using CodeGenerator.Services.Interface;
using Jt.Common.Tool.Extension;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public class CodeGenSchemeViewModel : ListViewModelBase<CodeGenSchemeDto, CodeGenScheme>
    {
        private readonly ICodeGenSchemeSvc _service;
        public CodeGenSchemeViewModel(IRegionManager regionManager, IDialogHostService dialogHostService, IEventAggregator eventAggregator, ICodeGenSchemeSvc schemeSvc, ICodeGenSchemeSvc service) : base(dialogHostService, eventAggregator, regionManager, schemeSvc)
        {
            _service = service;
        }

        protected override string AddViewDialogName { get; set; } = "CodeGenSchemeEditView";
        protected override string EditViewDialogName { get; set; } = "CodeGenSchemeEditView";

        protected async override Task<ButtonResult> InsertBtn()
        {
            try
            {
                Item = new CodeGenSchemeDto();

                NavigationParameters navigationParameters = new NavigationParameters()
            {
                { "Title", "新增" },
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

        protected async override Task<ButtonResult> EditBtn()
        {
            try
            {
                if (ListItems.Count(x => x.IsSelected) != 1)
                {
                    _eventAggregator.SendMessage("请选择一条数据");
                    return ButtonResult.Cancel;
                }

                Item = ListItems.FirstOrDefault(x => x.IsSelected).DeepCopyBySerialize();

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
                var result = await base.DelBtn();
                if (result == ButtonResult.Cancel)
                {

                    return ButtonResult.Cancel;
                }

                bool isok = await _service.DelCodeSchemeAsync(_delIds);
                LoadAsync();

            }
            catch (Exception ex)
            {
                _logger.Error(ex, "SelectedDatabase");
                _eventAggregator.SendMessage(ex.ToString());
            }
            return ButtonResult.OK;
        }
    }
}
