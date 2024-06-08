using CodeGenerator.Core.Dialog;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MaterialDesignThemes.Wpf;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Core.Mvvm
{
    public partial class HostDialogViewModelBase : ViewModelBase, IDialogHostViewModelBase
    {
        public string DialogHostName { get; set; }

        [ObservableProperty]
        public string _title;

        [RelayCommand]
        public virtual void Cancel()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.Cancel));
            }
        }

        [RelayCommand]
        public virtual void Save()
        {
            if (DialogHost.IsDialogOpen(DialogHostName))
            {
                DialogHost.Close(DialogHostName, new DialogResult(ButtonResult.OK));
            }
        }

        public virtual void OnDialogOpen(IDialogParameters parameters)
        {
            if (parameters.ContainsKey("Title"))
            {
                Title = parameters.GetValue<string>("Title");
            }
        }
    }
}
