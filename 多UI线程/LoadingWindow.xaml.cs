﻿using System;
using System.Threading;
using System.Windows;

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
            _ = new Timer(_ => Dispatcher.InvokeAsync(() => textBlock.Text = $"{DateTime.Now:HH:mm:ss.ffff}"),
                null, 0, 10);
        }
    }
}
