using OpenQA.Selenium.Chrome;
using Raize.CodeSiteLogging;
using System;
using System.IO;
using System.Net;
using System.Text;

namespace 破解爬虫防护
{
    public static class Jsl
    {
        public static string GetCookies(HttpWebResponse httpWebResponse)
        {
            var script1 = "";
            var script2 = "";
            var chromeDriver = new ChromeDriver();
            try
            {
                using (var responseStream = httpWebResponse.GetResponseStream())
                using (var streamReader = new StreamReader(responseStream ?? throw new InvalidOperationException(), Encoding.UTF8))
                    script1 = streamReader.ReadToEnd();
                chromeDriver.Navigate().GoToUrl(httpWebResponse.ResponseUri);
                var run = script1.Substring("<script>", "</script>").Replace("{eval(", "{return (");
                run = $"function script2(){{{run}}}; return script2();";
                script2 = chromeDriver.ExecuteScript(run).ToString();
                var del = script2.Substring("=function(){", "document.cookie=");
                run = script2.Replace(del + "document.cookie=", "return ");
                var name = script2.Substring("var ", "=");
                run = $"{run}; return {name}();";
                var cookie = chromeDriver.ExecuteScript(run).ToString();
                return cookie;
            }
            catch (Exception ex)
            {
                ex.SendCodeSite();
                CodeSite.Send("script1", script1);
                CodeSite.Send("script2", script2);
                return "";
            }
            finally
            {
                chromeDriver.Quit();
            }
        }
    }
}
