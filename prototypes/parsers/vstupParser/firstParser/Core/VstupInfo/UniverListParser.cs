using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AngleSharp.Dom.Html;

namespace firstParser.Core.Habra
{
    class UniverListParser : IParser<string[]>
    {
        public string[] Parse(IHtmlDocument document)
        {
            List<string> list = new List<string>();
            //var items = document.QuerySelectorAll("table").Where(item => item.ClassName != null && item.ClassName.Contains("tablesaw tablesaw-stack"));//лінк на сторнку
            // set new settings


            //var items = document.QuerySelectorAll("table").Where(itemm => itemm.InnerHtml.Contains("vnzt0"));
            var items = document.QuerySelectorAll("table").Where(item => item.InnerHtml != null && (item.OuterHtml.Contains("vnzt1")|| item.OuterHtml.Contains("vnzt0")|| item.OuterHtml.Contains("vnzt2")));
            //var items = document.QuerySelectorAll("td").Where(item => item.InnerHtml != null && item.InnerHtml.Contains("href="));

            var items2=items.Where(item => item.InnerHtml != null && item.InnerHtml.Contains("href="));
            foreach (var item in items)
            {
                string last=null;
                //list.Add(item.TextContent);
                string[] text = item.InnerHtml.Split('"');
                foreach(string t in text)
                {

                    if (t.Contains("html"))
                    {
                        if (t[0] == '.')
                        {
                            last = t.Remove(0, 1);
                            list.Add(last);
                        }
                    }
                }
                
                //if (last!=null)
                   // list.Add(last);
                //list.Add(item.InnerHtml.ToString().Trim());
            }
            return list.ToArray();
        }
    }
}
