using System.IO;

namespace TreeView虚拟化跳转
{
    public class MainWindowViewModel
    {
        public MainWindowViewModel()
        {
            Root.IsExpanded = true;
        }

        public TreeViewItemViewModel Root { get; } = TreeViewItemViewModel.Create(new DirectoryInfo(@"C:\"));
    }
}
