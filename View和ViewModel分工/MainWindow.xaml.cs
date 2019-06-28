using Microsoft.Win32;
using System.Windows;
using System.Windows.Controls;

namespace View和ViewModel分工
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
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (sender is Button button && openFileDialog.ShowDialog() == true)
                button.CommandParameter = openFileDialog.FileName;
            else
                throw new UserCanceledException();
        }
    }
}
