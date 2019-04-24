using OpenQA.Selenium.Chrome;
using Raize.CodeSiteLogging;
using ScrapySharp.Network;
using System;
using System.Net;
using System.Threading.Tasks;
using System.Windows;

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
