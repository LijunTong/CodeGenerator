using CodeGenerator.Core.Mvvm;
using CodeGenerator.Data.Dto;
using CommunityToolkit.Mvvm.ComponentModel;
using Jt.Common.Tool.Extension;
using Prism.Events;
using Prism.Services.Dialogs;
using System.Collections.ObjectModel;

namespace CodeGenerator.Modules.Code.ViewModels
{
    public partial class CodeDbEditViewModel : FormEditDialogViewModelBase<CodeDbDto>
    {
        [ObservableProperty]
        private ObservableCollection<string> _typeList;

        private IEventAggregator _eventAggregator;

        public CodeDbEditViewModel(IEventAggregator eventAggregator)
        {
            TypeList = new ObservableCollection<string>()
                {
                    "MySql","SqlServer","PostgreSQL","Sqlite"
                };

            _eventAggregator = eventAggregator;

            if (FormData == null)
            {
                FormData = new CodeDbDto();
            }

        }

        public override void Save()
        {
            if (!(FormData is CodeDbDto form))
            {
                throw new ArgumentException("FormData数据类型不正确");
            }

            if (form.Name.IsNullOrWhiteSpace())
            {
                //_eventAggregator.SendMessage("名称不能为空");
                return;
            }

            if (form.Type.IsNullOrWhiteSpace())
            {
                //_eventAggregator.SendMessage("类型不能为空");
                return;
            }

            if (form.ConStr.IsNullOrWhiteSpace())
            {
                //_eventAggregator.SendMessage("连接字符串不能为空");
                return;
            }

            base.Save();
        }

        public override void OnDialogOpen(IDialogParameters parameters)
        {
            FormData.Type = TypeList[0];
            base.OnDialogOpen(parameters);
        }
    }
}
