using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel.DataAnnotations;

namespace 数据验证
{
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class MainWindowViewModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        public virtual string UserName { get; set; }
    }
}
