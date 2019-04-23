using Jint;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Net;
using System.Text;
using Raize.CodeSiteLogging;

namespace 破解爬虫防护
{
    public static class Jsl
    {
        public static string GetCookies(HttpWebResponse httpWebResponse)
        {
            string script1 = "";
            string script2 = "";
            try
            {
                using (var responseStream = httpWebResponse.GetResponseStream())
                using (var streamReader = new StreamReader(responseStream ?? throw new InvalidOperationException(), Encoding.UTF8))
                    script1 = streamReader.ReadToEnd();
                var engine = new Engine().SetValue("document", new Document());

                var run = script1.Substring("<script>", "</script>").Replace("{eval(", "{return (");
                run = $"function script2(){{{run}}};";
                script2 = engine.Execute(run).GetValue("script2").Invoke().ToString();

                var del = script2.Substring("=function(){", "document.cookie=");
                run = script2.Replace(del + "document.cookie=", "return ");
                var name = script2.Substring("var ", "=");
                return engine.Execute(run).GetValue(name).Invoke().ToString();
            }
            catch (Exception ex)
            {
                ex.SendCodeSite();
                CodeSite.Send("script1", script1);
                CodeSite.Send("script2", script2);
                return "";
            }
        }

        [SuppressMessage("ReSharper", "InconsistentNaming")]
        [SuppressMessage("ReSharper", "UnusedMember.Local")]
        [SuppressMessage("ReSharper", "UnusedParameter.Local")]
        private class Document
        {
            public void attachEvent(object o1, object o2) { }
        }
    }
}
