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

namespace 输入法正在输入
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

        bool isComposition;//输入法正在输入，有虚线状态

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!isComposition) textBlock.Text = textBox.Text;
        }

        private void TextBox_PreviewTextInputStart(object sender, TextCompositionEventArgs e)
        {
            isComposition = true;
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            isComposition = false;
        }
    }
}
