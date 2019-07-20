using System;
using System.Windows;
using System.Windows.Threading;

namespace 多UI线程
{
    /// <summary>
    /// LoadingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LoadingWindow : Window
    {
        public LoadingWindow()
        {
            InitializeComponent();
            var timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(10),
                IsEnabled = true
            };
            timer.Tick += (s, e) => textBlock.Text = $"{DateTime.Now:HH:mm:ss.ffff}";
        }
    }
}
