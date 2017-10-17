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
        //HtmlNode RetreiveNode(HtmlNode univNode, string xPath);
        District GetDistrict(int ID, string districtName);

        University GetUniversityInfo(int id, int districtID, string name, string adress, string webSite);
        //IEnumerable<Faculty> RetreiveFaculties(string url, string xPath);
        IEnumerable<Speciality> GetSpecialityInfo(IEnumerable<HtmlNode> nodes, Dictionary<string, string> specFields);

        string Url { get;  set; }
        
    }

    interface IErrorsLog
    {
        void StartLog();
        void EndLog();
    }

    class Parser :IParser
    {

        private string url;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        HtmlNodeCollection nodes;

        public Parser(string url)
        {
            this.url = url;
        }

        public void ChangeUrl(string url)
        {
            this.url = url; 
        }

        public District GetDistrict(int ID, string districtName)
        {
            return new District(ID, districtName);
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
 

        
        //404 not found
        public static bool IsAvailable(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }

            catch (WebException ex)
            {
                using (StreamWriter sw = new StreamWriter("errorlog.txt", true) )
                {
                    sw.WriteLine(String.Format("date:{0}; time{1}; error:{2} - {3} ",
                                                DateTime.Now.Date, DateTime.Now.TimeOfDay,  url, (((HttpWebResponse)ex.Response).StatusCode)));
                    sw.WriteLine();
                }
                return false;
            }
        }

        public University GetUniversityInfo(int id, int districtID, string name, string adress, string webSite)
        {
            University university = new University(id, districtID, name, adress, webSite);
            return university;
        }

        public HtmlNode RetreiveNode(string xPath)
        {
            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(GetDocument());
            HtmlNode node = doc.DocumentNode.SelectSingleNode(xPath);
            return node;
        }

        //public HtmlNode RetreiveNode(HtmlNode node, string xPath)
        //{
        //    return node.SelectSingleNode(xPath);
        //}

        public IEnumerable<Speciality> GetSpecialityInfo(IEnumerable<HtmlNode> nodes, Dictionary<string,string> specFields)
        {
            List<Speciality> specialities = new List<Speciality>();

            foreach (HtmlNode node in nodes)
            {
                //some fields on website are empty
                string name,m1,m2;
                if (node.SelectSingleNode(specFields["SpecDirectionNodes"]) != null)
                    name = node.SelectSingleNode(specFields["SpecDirectionNodes"]).InnerText;
                else
                    name = "NULL";

                if (node.SelectSingleNode(specFields["SpecSpecNode"]) != null)
                    m1 = node.SelectSingleNode(specFields["SpecSpecNode"]).InnerText;
                else
                    m1 = "NULL";

                if (node.SelectSingleNode(specFields["SpecFacNode"]) == null)
                    m2 = "NULL";
                else
                    m2 = node.SelectSingleNode(specFields["SpecFacNode"]).InnerText;

                specialities.Add(new Speciality()
                {
                   Name = name,
                   AvgPoints = m1,
                   MinPoints = m2
                }
                    );
               
 
            }
             return specialities;           
        }

     
    }

    class ErrorsLog: IErrorsLog
    {
        DateTime start;
        DateTime end;

        public void StartLog()
        {
            start = DateTime.Now;
            using (StreamWriter sw = new StreamWriter("errorlog.txt", true))
            {
                sw.WriteLine(String.Format("Parsing started at . . . date:{0}; time{1};",
                                            DateTime.Now.Date, DateTime.Now.TimeOfDay));
                sw.WriteLine();
            }
        }

        public void EndLog()
        {
            end = DateTime.Now;
            using (StreamWriter sw = new StreamWriter("errorlog.txt", true))
            {
                sw.WriteLine(String.Format("Parsing completed at . . . date:{0}; time{1};",
                                            end.Date, end.TimeOfDay));
                //sw.WriteLine("Parsing duration: {0}:{1}:{2}");
            }
        }        
    }
}
