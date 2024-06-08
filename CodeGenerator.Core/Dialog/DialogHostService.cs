using MaterialDesignThemes.Wpf;
using Prism.Ioc;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CodeGenerator.Core.Dialog
{
    public class DialogHostService : IDialogHostService
    {
        private readonly IContainerExtension _containerExtension;

        public DialogHostService(IContainerExtension containerExtension)
        {
            _containerExtension = containerExtension;
        }

        public async Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root")
        {
            if (parameters == null)
            {
                parameters = new DialogParameters();
            }

            // 获取name对应的窗口对象
            var content = _containerExtension.Resolve<object>(name);
            if (!(content is FrameworkElement view))
            {
                throw new ArgumentException("name对应的实例不是控件");
            }

            // 绑定view的DataContext
            if (view.DataContext == null && ViewModelLocator.GetAutoWireViewModel(view) == null)
            {
                ViewModelLocator.SetAutoWireViewModel(view, true);
            }

            if (!(view.DataContext is IDialogHostViewModelBase viewModel))
            {
                throw new ArgumentException("name控件对应的DataContext没有实现IDialogHostAware");
            }

            viewModel.DialogHostName = dialogHostName;
            DialogOpenedEventHandler handler = (sender, args) =>
            {
                viewModel.OnDialogOpen(parameters);
                args.Session.UpdateContent(view);
            };

            if (!DialogHost.IsDialogOpen(dialogHostName))
            {
                return (IDialogResult)await DialogHost.Show(view, dialogHostName, handler);
            }
           else
            {
                return new DialogResult(ButtonResult.OK);
            }
        }


        public void CloseDialog(string name, string dialogHostName = "Root", IDialogResult result = null)
        {
            // 获取name对应的窗口对象
            var content = _containerExtension.Resolve<object>(name);
            if (!(content is FrameworkElement view))
            {
                throw new ArgumentException("name对应的实例不是控件");
            }

            // 绑定view的DataContext
            if (view.DataContext == null && ViewModelLocator.GetAutoWireViewModel(view) == null)
            {
                ViewModelLocator.SetAutoWireViewModel(view, true);
            }

            if (!(view.DataContext is IDialogHostViewModelBase viewModel))
            {
                throw new ArgumentException("name控件对应的DataContext没有实现IDialogHostAware");
            }

            viewModel.DialogHostName = dialogHostName;
            if (result == null)
            {
                result = new DialogResult(ButtonResult.OK);
            }

            if(DialogHost.IsDialogOpen(dialogHostName))
            {
                DialogHost.Close(dialogHostName, result);
            }
        }
    }
}
