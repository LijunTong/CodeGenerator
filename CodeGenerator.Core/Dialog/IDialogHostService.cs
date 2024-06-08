using Prism.Services.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeGenerator.Core.Dialog
{
    public interface IDialogHostService
    {
        /// <summary>
        /// 显示对话框
        /// </summary>
        /// <param name="name">内容名称，能够通过名称在ioc容器找到</param>
        /// <param name="parameters">参数</param>
        /// <param name="dialogHostName">在哪个host显示</param>
        /// <returns></returns>
        Task<IDialogResult> ShowDialog(string name, IDialogParameters parameters, string dialogHostName = "Root");

        /// <summary>
        /// 关闭对话框
        /// </summary>
        /// <param name="name">内容名称，能够通过名称在ioc容器找到</param>
        /// <param name="dialogHostName">在哪个host显示</param>
        /// <param name="result">返回结果</param>
        void CloseDialog(string name, string dialogHostName = "Root", IDialogResult result = null);
    }
}
