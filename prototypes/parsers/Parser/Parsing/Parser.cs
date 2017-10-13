using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using HtmlAgilityPack;
using Parsing.DataClasses;

namespace Parsing
{
    interface IParser
    {
        void ChangeUrl(string url);
        HtmlNodeCollection RetreiveNodes(string xPath);
        HtmlNode RetreiveNode(string xPath);
        HtmlNode RetreiveNode(HtmlNode univNode, string xPath);
        District GetDistrict(HtmlNode node);

        University GetUniversityInfo(HtmlNode node);
        //IEnumerable<Faculty> RetreiveFaculties(string url, string xPath);
        IEnumerable<Speciality> GetSpecialityInfo(IEnumerable<HtmlNode> nodes, Dictionary<string, string> specFields);

        
    }
    class Parser :IParser
    {
     
        private string url;
        private string xPath;
        HtmlNodeCollection nodes;

        public Parser(string url)
        {
            this.url = url;
        }

        public void ChangeUrl(string url)
        {
            this.url = url; 
        }

        public District GetDistrict(HtmlNode node)
        {
            return new District(node.InnerText);
        }

        public string GetDocument()
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        return reader.ReadToEnd();
                    }
                }
            }
        }

     

        public HtmlNodeCollection RetreiveNodes(string xPath)
        {

            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(GetDocument());
            nodes = doc.DocumentNode.SelectNodes(xPath);
            return nodes;
        }
 

        
        //not found
        public static bool IsAvailable(string link)
        {
            try
            {
                WebRequest request = WebRequest.Create(link);
                using (WebResponse response = request.GetResponse())
                {
                    return true;
                }
            }

            catch (WebException ex)
            {
                using (StreamWriter sw = new StreamWriter("errorlog.txt"))
                {

                    sw.WriteLine(String.Format("date:{0}; time{1}; erroe:{2}",
                                                DateTime.Now.Date, DateTime.Now.TimeOfDay, ex.Status.ToString()));

                }
                return false;
            }
        }




        public University GetUniversityInfo(HtmlNode node)
        {
            University university = new University();
            university.Name = node.InnerText;
            return university;
        }

        public HtmlNode RetreiveNode(string xPath)
        {
            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(GetDocument());
            HtmlNode node = doc.DocumentNode.SelectSingleNode(xPath);
            return node;
        }

        public HtmlNode RetreiveNode(HtmlNode node, string xPath)
        {
            return node.SelectSingleNode(xPath);
        }

        public IEnumerable<Speciality> GetSpecialityInfo(IEnumerable<HtmlNode> nodes, Dictionary<string,string> specFields)
        {
            List<Speciality> specialities = new List<Speciality>();

            foreach (HtmlNode node in nodes)
            {
                specialities.Add(new Speciality()
                {
                    Name = node.SelectSingleNode(specFields["Галузь"]).InnerText,
                    MyProperty = node.SelectSingleNode(specFields["Спеціальність"]).InnerText,
                    MyProperty2 = node.SelectSingleNode(specFields["Факультет"]).InnerText
                }
                    );
               
 
            }
             return specialities;
            
        }

       
    }
}
