using CodeGenerator.Core.Dialog;
using CodeGenerator.Core.Event;
using CodeGenerator.ViewModels;
using MaterialDesignThemes.Wpf;
using Prism.Events;
using System;
using System.Windows;
using System.Windows.Input;

namespace CodeGenerator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private IEventAggregator _aggregator;
        private IDialogHostService _dialogHostService;
        public MainWindow(IEventAggregator aggregator, IDialogHostService dialogHostService)
        {
            InitializeComponent();

            this.minBtn.Click += (s, e) =>
            {
                this.WindowState = WindowState.Minimized;
            };

            this.maxBtn.Click += (s, e) =>
            {
                var data = this.DataContext as MainWindowViewModel;
                if (this.WindowState == WindowState.Maximized)
                {
                    this.WindowState = WindowState.Normal;
                    data.MaxIconKind = "WindowMaximize";
                }
                else
                {
                    this.WindowState = WindowState.Maximized;
                    data.MaxIconKind = "WindowRestore";
                }
            };

            this.closeBtn.Click += (s, e) =>
            {
                this.Close();
            };

            this.colorZone.MouseDoubleClick += (s, e) =>
            {
                //if (this.WindowState == WindowState.Maximized)
                //{
                //    this.WindowState = WindowState.Normal;
                //}
                //else
                //{
                //    this.WindowState = WindowState.Maximized;
                //}
            };

            this.colorZone.MouseMove += (s, e) =>
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    this.DragMove();
                }
            };

            _aggregator = aggregator;

            _aggregator.RegisterMessage(msg =>
            {
                Snackbar.MessageQueue.Enqueue(msg,
                    null,
                    null,
                    null,
                        promote: true,
            true,
            TimeSpan.FromSeconds(2));
            });


            _dialogHostService = dialogHostService;
            _aggregator.RegisterLoading(async isShow =>
            {
                if (isShow)
                {
                    await _dialogHostService.ShowLoading();
                }
                else
                {
                    _dialogHostService.CloseLoading();
                }
            });
        }

        private void Window_StateChanged(object sender, System.EventArgs e)
        {
            //if (WindowState == WindowState.Maximized)
            //{
            //    var screen = System.Windows.SystemParameters.WorkArea;
            //    this.MaxWidth = screen.Width + 10;
            //    this.MaxHeight = screen.Height + 10;
            //}
        }
    }
}
