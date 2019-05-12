using System.Windows;

namespace xaml编译错误
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            label.Content = Properties.Resources.Border;
        }
    }
}
