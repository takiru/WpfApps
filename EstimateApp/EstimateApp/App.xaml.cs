using System.Configuration;
using System.Data;
using System.Windows;

namespace EstimateApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            // グローバル例外ハンドラーを設定
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;
        }

        private void App_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            MessageBox.Show($"予期しないエラーが発生しました。\n\n{e.Exception.Message}\n\n詳細:\n{e.Exception.StackTrace}",
                "エラー", MessageBoxButton.OK, MessageBoxImage.Error);

            e.Handled = true;
        }
    }
}