using DevExpress.Mvvm.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace 数据验证
{
    [POCOViewModel(ImplementIDataErrorInfo = true)]
    public class MainWindowViewModel
    {
        [Required(ErrorMessage = "请输入用户名")]
        public virtual string UserName { get; set; }

        public void Submit() { }

        public bool CanSubmit() => !HasErrors;

        public bool HasErrors => this is IDataErrorInfo dataErrorInfo
                && GetType().GetProperties().Any(f => !string.IsNullOrEmpty(dataErrorInfo[f.Name]));
    }
}
