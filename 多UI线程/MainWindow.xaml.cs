using System.Threading;
using System.Windows;

namespace 多UI线程
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void 普通方式_Click(object sender, RoutedEventArgs e)
        {
            var loadingWindow = new LoadingWindow();
            loadingWindow.Show();
            耗时操作();
            loadingWindow.Close();
        }

        private static void 耗时操作() => Thread.Sleep(5000);

        private void 单独UI线程_Click(object sender, RoutedEventArgs e)
        {
            LoadingWindow loadingWindow = null;
            var thread = new Thread(() =>
            {
                loadingWindow = new LoadingWindow();
                loadingWindow.Show();
                System.Windows.Threading.Dispatcher.Run();
            });
            thread.SetApartmentState(ApartmentState.STA);
            thread.Start();
            耗时操作();
            loadingWindow.Dispatcher.Invoke(() => loadingWindow.Close());
            loadingWindow.Dispatcher.InvokeShutdown();
        }
    }
}
