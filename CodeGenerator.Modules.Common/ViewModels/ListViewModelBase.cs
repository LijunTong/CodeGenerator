using CodeGenerator.Core.Dialog;
using CodeGenerator.Core.Event;
using CodeGenerator.Data.Dto;
using CodeGenerator.Data.Entity;
using CodeGenerator.Services.Interface;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Jt.Common.Tool.Extension;
using Prism.Events;
using Prism.Regions;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;

namespace CodeGenerator.Core.Mvvm
{
    public abstract partial class ListViewModelBase<TDto, TEntity> : RegionViewModelBase where TDto : BaseDto where TEntity : BaseEntity
    {
        protected IBaseSvc<TEntity> _svc;
        protected IDialogHostService _dialogHostService;
        protected IEventAggregator _eventAggregator;

        protected abstract string AddViewDialogName { get; set; }
        protected abstract string EditViewDialogName { get; set; }

        [ObservableProperty]
        protected ObservableCollection<TDto> _listItems;

        [ObservableProperty]
        protected TDto _item;

        private List<TEntity> _dataList;

        protected List<string> _delIds;

        public ListViewModelBase(IDialogHostService dialogHostService, IEventAggregator eventAggregator, IRegionManager regionManager, IBaseSvc<TEntity> svc) : base(regionManager)
        {
            _dialogHostService = dialogHostService;
            _eventAggregator = eventAggregator;
            _svc = svc;
        }

        public override void OnNavigatedTo(NavigationContext navigationContext)
        {
            base.OnNavigatedTo(navigationContext);
            LoadAsync();

        }

        protected virtual void LoadAsync()
        {
            //_eventAggregator.ToggleLoading(true);
            try
            {
                GetListAsync().Wait();
            }
            catch (Exception ex)
            {
                _eventAggregator.SendMessage("加载失败");
                _logger.Error(ex);
            }
            finally
            {
                //_eventAggregator.ToggleLoading(false);

            }

        }

        protected virtual async Task GetListAsync()
        {
            _dataList = await _svc.GetListAsync(x => x.IsDel == 0);
            ListItems = new ObservableCollection<TDto>();
            foreach (var item in _dataList.OrderByDescending(x=>x.CreateTime))
            {
                ListItems.Add(item.CopyValue<TEntity, TDto>());
            }
        }

        [RelayCommand]
        protected virtual async Task<ButtonResult> InsertBtn()
        {
            Item = (TDto)Activator.CreateInstance(typeof(TDto));
            DialogParameters keyValuePairs = new DialogParameters()
            {
                { "Title", "新增" },
                {"FormData", Item }
            };

            var result = await _dialogHostService.ShowDialog(AddViewDialogName, keyValuePairs);
            return result.Result;
        }

        [RelayCommand]
        protected virtual async Task<ButtonResult> EditBtn()
        {
            if (ListItems.Count(x => x.IsSelected) != 1)
            {
                _eventAggregator.SendMessage("请选择一条数据");
                return ButtonResult.Cancel;
            }

            Item = ListItems.FirstOrDefault(x => x.IsSelected).DeepCopyBySerialize();

            DialogParameters keyValuePairs = new DialogParameters()
            {
                { "FormData", Item },
                { "Title", "编辑" }
            };

            var result = await _dialogHostService.ShowDialog(EditViewDialogName, keyValuePairs);

            return result.Result;
        }

        [RelayCommand]
        protected virtual async Task<ButtonResult> DelBtn()
        {
            try
            {
                var selectNum = ListItems.Where(x => x.IsSelected).Select(x => x.Id).ToList();
                if (!selectNum.Any())
                {
                    _eventAggregator.SendMessage("请至少选择一条数据");
                    return ButtonResult.Cancel;
                }

                _delIds = selectNum;
                var result = await _dialogHostService.ShowComfirmDialog($"确认删除 {selectNum.Count()} 条记录吗？删除后将无法恢复！");
                return result.Result;
            }
            catch (Exception ex)
            {
                _logger.Error(ex
                    , "DelBtn");
                _eventAggregator.SendMessage("操作失败");
                return ButtonResult.Cancel;
            }
        }
    }
}
