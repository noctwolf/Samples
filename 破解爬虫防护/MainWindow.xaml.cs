using Raize.CodeSiteLogging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
using ScrapySharp.Network;

namespace 破解爬虫防护
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
            Task.Run(Test);
        }

        private void Test()
        {
            CodeSite.EnterMethod(this, "Test");
            ScrapingBrowser scrapingBrowser = new ScrapingBrowser();
            scrapingBrowser.IgnoreCookies = true;
        re:
            try
            {
                var s = scrapingBrowser.NavigateTo(new Uri("https://www.yidaiyilu.gov.cn/info/iList.jsp?cat_id=10009"), HttpVerb.Get, "");
                CodeSite.Send("s", s);
            }
            catch (Exception ex)
            {
                if (ex is WebException we && we.Response is HttpWebResponse hwr)
                {
                    if ((int)hwr.StatusCode == 521)
                    {
                        CodeSite.SendReminder("破解爬虫防护");
                        scrapingBrowser.SetCookies(new Uri("https://www.yidaiyilu.gov.cn/"), Jsl.GetCookies(hwr));
                        goto re;
                    }
                }
            }
            finally
            {
                CodeSite.ExitMethod(this, "Test");
            }
        }
    }
}
