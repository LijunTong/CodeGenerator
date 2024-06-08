using CommunityToolkit.Mvvm.ComponentModel;
using NLog;
using Prism.Services.Dialogs;

namespace CodeGenerator.Core.Mvvm
{
    public partial class FormEditDialogViewModelBase<TDto> : HostDialogViewModelBase
    {
        [ObservableProperty]
        private TDto _formData;

        protected Logger _logger = LogManager.GetCurrentClassLogger();

        public override void OnDialogOpen(IDialogParameters parameters)
        {
            base.OnDialogOpen(parameters);

            if (parameters.ContainsKey("FormData"))
            {
                FormData = parameters.GetValue<TDto>("FormData");
            }
        }
    }
}
