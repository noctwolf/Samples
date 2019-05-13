using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace 依赖属性更改事件
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DependencyPropertyDescriptor.FromProperty(TextBox.TextProperty, typeof(TextBox))
                .AddValueChanged(textBox, TextChanged);
        }

        private void TextChanged(object sender, EventArgs e)
        {
            textBlock.Text = textBox.Text;
        }
    }
}
