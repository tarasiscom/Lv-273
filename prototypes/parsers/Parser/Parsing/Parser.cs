using HtmlAgilityPack;
using Parsing.DataClasses;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Parsing
{
    public class Parser : IParser
    {
        private string url;
        private HtmlNodeCollection nodes;
        private List<Direction> directions;
        private List<Speciality> specialities;
        private List<University> universities;

        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        public Parser(string url)
        {
            this.url = url;
            this.directions = new List<Direction>();
            this.specialities = new List<Speciality>();
            this.universities = new List<University>();
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

        public void GetInfo(ref int id, ref int idDirection, int idUniv, string district, HtmlNode univNode, IEnumerable<HtmlNode> nodes, Dictionary<string, string> specFields)
        {
            
            string namePrevDirection = string.Empty;
            string namePrevSpeciality = string.Empty;
            string nameDirection;
            string nameSpeciality;

            universities.Add(new University(idUniv, district, univNode.SelectSingleNode(specFields["UniversitiesNamesNode"]).InnerText, univNode.SelectSingleNode(specFields["UniversitiesAdressNode"]).InnerText, univNode.SelectSingleNode(specFields["UniversitiesWebSitesNode"]).InnerText));

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


                if (!directions.Exists((direction) => direction.Name == nameDirection))
                {
                    directions.Add(new Direction(idDirection, nameDirection));
                    idDirection++;

                    if (nameSpeciality != namePrevSpeciality)
                    {
                        specialities.Add(new Speciality(id, idDirection - 1, idUniv, nameSpeciality));
                        id++;
                        namePrevSpeciality = nameSpeciality;
                    }
                }

                else
                {
                    if (nameSpeciality != namePrevSpeciality)
                    {
                        specialities.Add(new Speciality(id, directions.Find((direction) => direction.Name == nameDirection).ID, idUniv, nameSpeciality));
                        id++;
                        namePrevSpeciality = nameSpeciality;
                    }
                    
                }

                //if (nameSpeciality != namePrevSpeciality)
                //{
                //    specialities.Add(new Speciality(id, idDirection - 1, idUniv, nameSpeciality));
                //    id++;
                //    namePrevSpeciality = nameSpeciality;
                //}

                //else
                //{
                //    if (nameSpeciality != namePrevSpeciality)
                //    {
                //        //specialities.Add(new Speciality(id, directions.Find((dir) => dir.Name == nameDirection)).FacultyID, nameSpeciality);
                //        id++;
                //        namePrevSpeciality = nameSpeciality;
                //    }
                //}
                //if (nameDirection != namePrevDirection)
                //{
                //    directions.Add(new Direction(idDirection, nameDirection));
                //    idDirection++;
                //    namePrevDirection = nameDirection;
                //}

                //if (nameSpeciality != namePrevSpeciality)
                //{
                //    specialities.Add(new Speciality(id, idDirection - 1, nameSpeciality));
                //    id++;
                //    namePrevSpeciality = nameSpeciality;
                //}
                //}
            }
        }

        public IEnumerable<Direction> GetDirections()
        {
            return directions;
        }

        public List<Speciality> GetSpecialities()
        {
            return specialities;
        }

        public IEnumerable<University> GetUniversities()
        {
            return universities;
        }
    }
}
