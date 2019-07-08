using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ComboBox过滤
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

        public IEnumerable<string> DemoCollection => Enumerable.Range(1, 100).Select(f => Guid.NewGuid().ToString());

        private void ComboBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (sender is ComboBox comboBox)
            {
                var view = (CollectionView)CollectionViewSource.GetDefaultView(comboBox.ItemsSource);
                view.Filter = f => f is string s && s.Contains(comboBox.Text);
            }
        }
    }
}
