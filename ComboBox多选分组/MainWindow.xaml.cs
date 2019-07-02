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

namespace ComboBox多选分组
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

        public List<Demo> DemoList { get; } = Enumerable.Range(0, 10).Select(f => new Demo { Title = f, Group = f / 4 }).ToList();

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            var myView = (CollectionView)CollectionViewSource.GetDefaultView(DemoList);
            if (myView.CanGroup) myView.GroupDescriptions.Add(new PropertyGroupDescription("Group"));
        }
    }

    public struct Demo
    {
        public int Title { get; set; }

        public int Group { get; set; }
    }
}
