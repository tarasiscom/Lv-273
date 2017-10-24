using HtmlAgilityPack;
using Parsing.DataClasses;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Parsing
{
    class Parser : IParser
    {
        private string url;
        private HtmlNodeCollection nodes;
        private List<Direction> directions;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public Parser(string url)
        {
            this.url = url;
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
            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(GetDocument());
            nodes = doc.DocumentNode.SelectNodes(xPath);
            return nodes;
        }

        public HtmlNode RetreiveNode(string xPath)
        {
            HtmlDocument doc = new HtmlDocument();

            doc.LoadHtml(GetDocument());
            HtmlNode node = doc.DocumentNode.SelectSingleNode(xPath);
            return node;
        }

        public District GetDistrict(int ID, string districtName)
        {
            return new District(ID, districtName);
        }

        public University GetUniversityInfo(int id, string district, string name, string adress, string webSite)
        {
            return new University(id, district, name, adress, webSite);
        }

        public IEnumerable<Speciality> GetSpecialityInfo(ref int id, ref int idFac, int idUniv, IEnumerable<HtmlNode> nodes, Dictionary<string, string> specFields)
        {
            directions = new List<Direction>();
            List<Speciality> specialities = new List<Speciality>();
            string namePrevDirection = string.Empty;
            string namePrevSpeciality = string.Empty;
            string nameDirection;
            string nameSpeciality;

            foreach (HtmlNode node in nodes)
            {
                if (node.SelectSingleNode(specFields["SpecDirectionNode"]) == null)
                {
                    continue;
                }
                

                if (node.SelectSingleNode(specFields["SpecSpecNode"]) == null)
                {
                    continue;
                }
                nameSpeciality = node.SelectSingleNode(specFields["SpecSpecNode"]).InnerText;
                nameDirection = node.SelectSingleNode(specFields["SpecDirectionNode"]).InnerText;

                //delete spec code if it available
                int j;
                int counter = 0;
                for (int i = 0; i < nameSpeciality.Length; i++)
                {
                    if (int.TryParse(nameSpeciality[i].ToString(), out j) || nameSpeciality[i] == '.')
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

                //if (!directions.Exists((dir) => dir.Name == nameDirection))
                //{
                //    directions.Add(new Direction(idFac, idUniv, nameDirection));
                //    idFac++;
                //    namePrevDirection = nameDirection;

                //    if (nameSpeciality != namePrevSpeciality)
                //    {
                //        specialities.Add(new Speciality(id, idFac - 1, nameSpeciality));
                //        id++;
                //        namePrevSpeciality = nameSpeciality;
                //    }
                //}

                //else
                //{
                //    if (nameSpeciality != namePrevSpeciality)
                //    {
                //        specialities.Add(new Speciality(id, directions.Find((dir) => dir.Name == nameDirection)).FacultyID, nameSpeciality));
                //        id++;
                //        namePrevSpeciality = nameSpeciality;
                //    }
                //}
                if (nameDirection != namePrevDirection)
                {
                    directions.Add(new Direction(idFac, idUniv, nameDirection));
                    idFac++;
                    namePrevDirection = nameDirection;
                }

                if (nameSpeciality != namePrevSpeciality)
                {
                    specialities.Add(new Speciality(id, idFac - 1, nameSpeciality));
                    id++;
                    namePrevSpeciality = nameSpeciality;
                }
            }
            return specialities;
        }

        public IEnumerable<Direction> GetDirections()
        {
            return directions;
        }
    }
}
