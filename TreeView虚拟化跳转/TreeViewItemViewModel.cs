using DevExpress.Mvvm.POCO;
using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace TreeView虚拟化跳转
{
    public class TreeViewItemViewModel
    {
        public ObservableCollection<TreeViewItemViewModel> TreeViewItems { get; } = new ObservableCollection<TreeViewItemViewModel>();

        public TreeViewItemViewModel(DirectoryInfo directoryInfo)
        {
            DirectoryInfo = directoryInfo;
            TreeViewItems.Add(null);//虚拟子
        }

        public static TreeViewItemViewModel Create(DirectoryInfo directoryInfo)
        {
            return ViewModelSource.Create(() => new TreeViewItemViewModel(directoryInfo));
        }

        public DirectoryInfo DirectoryInfo { get; }

        public virtual bool IsExpanded { get; set; }

        protected void OnIsExpandedChanged(bool oldValue)
        {
            if (IsExpanded)
                try
                {
                    TreeViewItems.Clear();
                    DirectoryInfo.EnumerateDirectories().ToList().ForEach(f => TreeViewItems.Add(Create(f)));
                }
                catch (UnauthorizedAccessException)
                {
                    //无权限
                }
        }
    }
}
