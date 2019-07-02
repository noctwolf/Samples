using Microsoft.WindowsAPICodePack.Dialogs;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace TreeView虚拟化跳转
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
            CommonOpenFileDialog dialog = new CommonOpenFileDialog { IsFolderPicker = true };
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                BringIntoView(dialog.FileName);
                listBoxRecent.Items.Add(dialog.FileName);
            }
        }

        private void BringIntoView(string fileName)
        {
            var directoryInfo = new DirectoryInfo(fileName);
            var parents = new Stack<DirectoryInfo>();
            do { parents.Push(directoryInfo); }
            while ((directoryInfo = directoryInfo.Parent) != null);

            var parent = parents.Pop();
            if (parent.FullName != viewModel.Root.DirectoryInfo.FullName) return;//Root不一致

            var vm = viewModel.Root;
            ItemsControl itemsControl = treeView;
            while (parents.Any())
            {
                parent = parents.Pop();
                vm.IsExpanded = true;
                var data = vm.TreeViewItems.Select((f, index) => new { index, vm = f }).Single(f => f.vm.DirectoryInfo.FullName == parent.FullName);

                //虚拟化支持主要代码，就是以下5行。参考微软文档，去掉了一些不必要的代码
                //https://docs.microsoft.com/zh-cn/dotnet/framework/wpf/controls/how-to-find-a-treeviewitem-in-a-treeview
                itemsControl.ApplyTemplate();
                var itemsPresenter = FindVisualChild<ItemsPresenter>(itemsControl);
                itemsPresenter.ApplyTemplate();
                var virtualizingStackPanel = (VirtualizingStackPanel)VisualTreeHelper.GetChild(itemsPresenter, 0);
                virtualizingStackPanel.BringIndexIntoViewPublic(data.index);

                vm = data.vm;
                itemsControl = (ItemsControl)itemsControl.ItemContainerGenerator.ContainerFromItem(vm);
            }
            if (itemsControl is TreeViewItem treeViewItem) treeViewItem.IsSelected = true;
        }

        private void ListBoxRecent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var fileNames = e.AddedItems.OfType<string>().ToList();
            if (fileNames.Any()) BringIntoView(fileNames.First());
        }

        private T FindVisualChild<T>(Visual visual) where T : Visual
        {
            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(visual); i++)
            {
                var child = (Visual)VisualTreeHelper.GetChild(visual, i);
                if (child is T correctlyTyped) return correctlyTyped;
                var descendent = FindVisualChild<T>(child);
                if (descendent != null) return descendent;
            }
            return null;
        }
    }
}
