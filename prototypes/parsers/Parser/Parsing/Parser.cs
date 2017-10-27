using HtmlAgilityPack;
using Parsing.DataClasses;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Parsing
{
    public class Parser : IParser
    {
        private static int idSpeciality = 1;

        private static int idDirection = 1;

        private string url;

        public List<Direction> Directions { get; private set; }

        public List<Speciality> Specialities { get; private set; }

        public List<University> Universities { get; private set; }

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public Parser(string url)
        {
            this.url = url;
            this.Directions = new List<Direction>();
            this.Specialities = new List<Speciality>();
            this.Universities = new List<University>();
        }

        public void ChangeUrl(string url)
        {
            this.url = url;
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
            HtmlDocument doc = LoadHtmlDocument();
            return doc.DocumentNode.SelectNodes(xPath);
        }

        public HtmlNode RetreiveNode(string xPath)
        {
            HtmlDocument doc = LoadHtmlDocument();
            return doc.DocumentNode.SelectSingleNode(xPath);
        }

        public HtmlDocument LoadHtmlDocument()
        {
            HtmlDocument doc = new HtmlDocument();
            doc.LoadHtml(GetDocument());
            return doc;
        }

        private bool IsExistDirectionAndSpeciality(HtmlNode node, Dictionary<string, string> specFields)
        {
            if ((node.SelectSingleNode(specFields["SpecDirectionNode"]) == null) || (node.SelectSingleNode(specFields["SpecSpecNode"]) == null))
                return false;
            return true;
        }

        private string GetClearNameSpeciality(string nameSpeciality)
        {
            int counter = 0;
            for (int i = 0; i < nameSpeciality.Length; i++)
            {
                if (int.TryParse(nameSpeciality[i].ToString(), out int trash) || nameSpeciality[i] == '.')
                {
                    counter++;
                }
                else
                {
                    break;
                }
            }

            if (counter > 0)
            {
                nameSpeciality = nameSpeciality.Remove(0, counter).TrimStart(' ');
            }

            return nameSpeciality;
        }
        private bool IsDirectionAlreadyExists(string nameDirection)
        {
            if (Directions.Exists((direction) => direction.Name == nameDirection))
                return true;
            return false;
        }

        private string AddSpecialityIfNotExist(int idDirection, int idUniv, string nameSpeciality, string namePrevSpeciality)
        {
            if (nameSpeciality != namePrevSpeciality)
            {
                Specialities.Add(new Speciality(idSpeciality, idDirection - 1, idUniv, nameSpeciality));
                idSpeciality++;
                namePrevSpeciality = nameSpeciality;
            }
            return namePrevSpeciality;
        }

        public void GetInfo(int idUniv, string district, HtmlNode univNode, IEnumerable<HtmlNode> nodes, Dictionary<string, string> specFields)
        {
            
            string namePrevDirection = string.Empty;
            string namePrevSpeciality = string.Empty;
            string nameDirection;
            string nameSpeciality;

            Universities.Add(new University(idUniv, district, univNode.SelectSingleNode(specFields["UniversitiesNamesNode"]).InnerText, 
                                            univNode.SelectSingleNode(specFields["UniversitiesAdressNode"]).InnerText, 
                                            univNode.SelectSingleNode(specFields["UniversitiesWebSitesNode"]).InnerText));

            foreach (HtmlNode node in nodes)
            {
                if(!IsExistDirectionAndSpeciality(node, specFields))
                {
                    continue;
                }

                nameSpeciality = GetClearNameSpeciality(node.SelectSingleNode(specFields["SpecSpecNode"]).InnerText);
                nameDirection = node.SelectSingleNode(specFields["SpecDirectionNode"]).InnerText;

                if (!IsDirectionAlreadyExists(nameDirection))
                {
                    Directions.Add(new Direction(idDirection, nameDirection));
                    idDirection++;
                    namePrevSpeciality = AddSpecialityIfNotExist(idDirection - 1, idUniv, nameSpeciality, namePrevSpeciality);
                }
                else
                {
                    namePrevSpeciality = AddSpecialityIfNotExist(Directions.Find((direction) => direction.Name == nameDirection).ID, 
                                                                 idUniv, nameSpeciality, namePrevSpeciality);
                }
            }
        }
    }
}
