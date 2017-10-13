using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AngleSharp.Dom.Html;

namespace firstParser.Core.Habra
{
    class MainPageParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            List<string> list = new List<string>();
            //var items = document.QuerySelectorAll("table").Where(item => item.ClassName != null && item.ClassName.Contains("tablesaw tablesaw-stack"));//лінк на сторнку

            var items = document.QuerySelectorAll("td").Where(item => item.InnerHtml != null && item.InnerHtml.Contains("href="));

            foreach (var item in items)
            {
                string last=null;
                //list.Add(item.TextContent);
                string[] text = item.OuterHtml.Split('"');
                foreach(string t in text)
                {

                    if (t.Contains("html"))
                        last = t;
                }
                
                if (last!=null)
                    list.Add(last);
            }
            return list.ToArray();
        }
    }
}
