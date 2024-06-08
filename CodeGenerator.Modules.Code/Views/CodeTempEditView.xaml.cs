using CodeGenerator.Modules.Code.ViewModels;
using System.Windows.Controls;

namespace CodeGenerator.Modules.Code.Views
{
    /// <summary>
    /// Interaction logic for CodeTempEditView
    /// </summary>
    public partial class CodeTempEditView : UserControl
    {
        private readonly CodeTempEditViewModel _viewModel;
        public CodeTempEditView()
        {
            InitializeComponent();

            ICSharpCode.AvalonEdit.Search.SearchPanel.Install(TextEditor);

            _viewModel = DataContext as CodeTempEditViewModel;
            _viewModel.TextEditor = TextEditor;
        }

        private void MenuItem_Click(object sender, System.Windows.RoutedEventArgs e)
        {
            TextEditor.Paste();
        }

        private void Button_Click(object sender, System.Windows.RoutedEventArgs e)
        {

        }
    }
}
