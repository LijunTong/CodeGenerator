using CodeGenerator.Core.Mvvm;
using CommunityToolkit.Mvvm.ComponentModel;
using Prism.Services.Dialogs;

namespace CodeGenerator.Modules.Common.ViewModels
{
    public partial class ComComfirmDialogViewModel : HostDialogViewModelBase
    {
        [ObservableProperty]
        private string _msg;

        public ComComfirmDialogViewModel()
        {

        }

        public override void OnDialogOpen(IDialogParameters parameters)
        {
            base.OnDialogOpen(parameters);

            if (parameters.ContainsKey("Msg"))
            {
                Msg = parameters.GetValue<string>("Msg");
            }
        }
    }
}
