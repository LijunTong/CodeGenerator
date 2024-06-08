using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Core.Dialog
{
    public static class DialogHostServiceExt
    {
        public const string ComfirmDialog = "ComComfirmDialogView";
        public const string LoadingDialog = "LoadingView";

        public static async Task<IDialogResult> ShowComfirmDialog(this IDialogHostService dialogHostService, string msg, string dialogHostName = "Root")
        {
            IDialogParameters dialogParameters = new DialogParameters();
            dialogParameters.Add("Title", "请确认信息");
            dialogParameters.Add("Msg", msg);
            return await dialogHostService.ShowDialog(ComfirmDialog, dialogParameters, dialogHostName);
        }

        public static async Task ShowLoading(this IDialogHostService dialogHostService, string dialogHostName = "Root")
        {
            await dialogHostService.ShowDialog(LoadingDialog, null, dialogHostName);
        }

        public static void CloseLoading(this IDialogHostService dialogHostService, string dialogHostName = "Root")
        {
            dialogHostService.CloseDialog(LoadingDialog, dialogHostName);
        }
    }
}
