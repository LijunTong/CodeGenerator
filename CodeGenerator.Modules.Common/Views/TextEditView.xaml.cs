using CodeGenerator.Modules.Common.ViewModels;
using System.Windows.Controls;

namespace CodeGenerator.Modules.Common.Views
{
    /// <summary>
    /// TextEditView.xaml 的交互逻辑
    /// </summary>
    public partial class TextEditView : UserControl
    {
        public TextEditView(TextEditViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
        }
    }
}
