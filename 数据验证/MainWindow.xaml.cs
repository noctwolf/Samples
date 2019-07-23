using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace 数据验证
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

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (!this.All().Any(Validation.GetHasError)) return;
            MessageBox.Show("数据验证错误");
            throw new UserCanceledException();
        }
    }
}
