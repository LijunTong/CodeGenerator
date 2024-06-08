using Prism.Commands;
using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Core.Dialog
{
    public interface IDialogHostViewModelBase
    {
        string Title { get; set; }

        string DialogHostName { get; set; }

        /// <summary>
        /// 打开时方法
        /// </summary>
        /// <param name="parameters"></param>
        void OnDialogOpen(IDialogParameters parameters);

        /// <summary>
        /// 确定命令
        /// </summary>
        void Save();

        /// <summary>
        /// 取消命令
        /// </summary>
        void Cancel();
    }
}
