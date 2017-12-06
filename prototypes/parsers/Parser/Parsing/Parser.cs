using HtmlAgilityPack;
using Parsing.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace Parsing
{
    public class Parser : IParser
    {
        private static int idSpeciality = 1;
        private static int idDirection = 1;
        private static int idSubject = 1;
        private static int idSpecSub = 1;
        private string district;
        private HtmlNode universityNode;
        //private string year = "/2017";
        private string indexPage;
        private int universityID = 1;
        private int districtID = 1;
        private string url;
        private Dictionary<string, string> nodesXPaths;
        private HtmlNodeCollection specialitiesNodes;
        private HtmlNodeCollection universitiesNodes;
        private HtmlNodeCollection districtNodes;

        public List<Direction> Directions { get; private set; }

        public List<Speciality> Specialities { get; private set; }

        public List<University> Universities { get; private set; }

        public List<Subject> Subjects { get; private set; }

        public List<SpecSub> SpecSubs { get; private set; }

        public List<District> Districts { get; private set; }

        public Parser(Dictionary<string, string> specFields)
        {
            this.nodesXPaths = specFields;
            indexPage = nodesXPaths["URL"];
            url = indexPage;
            this.Directions = new List<Direction>();
            this.Specialities = new List<Speciality>();
            this.Universities = new List<University>();
            this.Subjects = new List<Subject>();
            this.SpecSubs = new List<SpecSub>();
            this.Districts = new List<District>();
        }

        private void ChangeUrl(string url)
        {
            this.url = url;
        }

        private string GetDocument()
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

        private HtmlNodeCollection RetreiveNodes(string xPath)
        {
            HtmlDocument doc = LoadHtmlDocument();
            return doc.DocumentNode.SelectNodes(xPath);
        }

        private HtmlNode RetreiveNode(string xPath)
        {
            HtmlDocument doc = LoadHtmlDocument();
            return doc.DocumentNode.SelectSingleNode(xPath);
        }

        private HtmlDocument LoadHtmlDocument()
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

        private string AddSpecialityIfNotExist(int idDirection, int idUniv, string nameSpeciality, string namePrevSpeciality, int applicationsAmount, int enrolledAmount, HtmlNode node)
        {
            if (nameSpeciality != namePrevSpeciality)
            {
                Specialities.Add(new Speciality(idSpeciality, idDirection, idUniv, ToUpper(nameSpeciality), applicationsAmount, enrolledAmount));
                AddSpecSub(node.SelectSingleNode(nodesXPaths["SubjectNode"]).InnerText);
                idSpeciality++;
                namePrevSpeciality = nameSpeciality;
            }
            return namePrevSpeciality;
        }

        private void GetInfo(int idDistrict, int idUniv, string district, HtmlNode univNode, IEnumerable<HtmlNode> nodes, Dictionary<string, string> specFields)
        {
            this.nodesXPaths = specFields;
            string namePrevDirection = string.Empty;
            string namePrevSpeciality = string.Empty;
            string nameDirection;
            string nameSpeciality;

            int applicationsAmount;
            int enrolledAmount;

            if (!Districts.Exists((dist) => dist.Name == district))
            {
                Districts.Add(new District(idDistrict, district));
            }

            //some universities has other paths to nodes
            if (univNode.SelectSingleNode(specFields["UniversitiesAdressNode"]).InnerText.Contains("</td>"))
            {
                Universities.Add(new University(idUniv, idDistrict, univNode.SelectSingleNode(specFields["UniversitiesNamesNode"]).InnerText,
                                                univNode.SelectSingleNode(specFields["UniversitiesAdressNode2"]).InnerText,
                                                GetSiteWithoutHttp(univNode.SelectSingleNode(specFields["UniversitiesWebSitesNode2"]).InnerText)));
            }
            else
            {
                Universities.Add(new University(idUniv, idDistrict, univNode.SelectSingleNode(specFields["UniversitiesNamesNode"]).InnerText,
                                               univNode.SelectSingleNode(specFields["UniversitiesAdressNode"]).InnerText,
                                               GetSiteWithoutHttp(univNode.SelectSingleNode(specFields["UniversitiesWebSitesNode"]).InnerText)));
            }

            foreach (HtmlNode node in nodes)
            {
                if(!IsPzso(node, specFields) || !IsExistDirectionAndSpeciality(node, specFields))
                {
                    continue;
                }

                nameSpeciality = GetClearNameSpeciality(node.SelectSingleNode(specFields["SpecSpecNode"]).InnerText);
                nameDirection = node.SelectSingleNode(specFields["SpecDirectionNode"]).InnerText;
                if (node.SelectSingleNode(specFields["SpecApplicationsNode"]) != null && node.SelectSingleNode(specFields["SpecEnrolledNode"]) != null)
                {
                    applicationsAmount = GetAmount(node.SelectSingleNode(specFields["SpecApplicationsNode"]).InnerText);
                    enrolledAmount = GetAmount(node.SelectSingleNode(specFields["SpecEnrolledNode"]).InnerText);
                }
                else
                {
                    applicationsAmount = 0;
                    enrolledAmount = -1;
                }

                if (!IsDirectionAlreadyExists(nameDirection))
                {
                    Directions.Add(new Direction(idDirection, nameDirection));
                    namePrevSpeciality = AddSpecialityIfNotExist(idDirection, idUniv, nameSpeciality, namePrevSpeciality, applicationsAmount, enrolledAmount, node);
                    idDirection++;
                }
                else
                {
                    namePrevSpeciality = AddSpecialityIfNotExist(Directions.Find((direction) => direction.Name == nameDirection).ID, 
                                                                 idUniv, nameSpeciality, namePrevSpeciality, applicationsAmount, enrolledAmount, node);
                }
            }
        }

        private string GetSiteWithoutHttp(string site)
        {
            if (site.Contains("http://") || site.Contains("https://"))
                return site;
            if (site.Contains("http//"))
                return site.Replace("http//", "http://");                  
            if (site.Contains("http:/"))
                return site.Replace("http:/", "http://");
            if (site.Contains("http:"))
                return site.Replace("http:", "http://");
            //if (int.TryParse(site.TrimStart('(').Remove(1), out int i))
            //    return site;
            return site.Insert(0, "http://");
        }

        private string ToUpper(string name) => name[0].ToString().ToUpper() + name.Remove(0, 1);

        //повна загальна середня освіта
        private bool IsPzso(HtmlNode node, Dictionary<string, string> specFields)
        {
            if (node.SelectSingleNode(specFields["SpecPZSONode"]) != null && node.SelectSingleNode(specFields["SpecPZSONode"]).InnerText == "ПЗСО")
                return true;
            return false;
        }

        private int GetAmount(string amountString)
        {
            if (int.TryParse(amountString.Replace("заяв:&nbsp;", "").Replace("зарах.:",""), out int amount))
                return amount;
            return -1;
        }

        private void AddSpecSub(string subjectsList)
        {
            string[] subjects = GetSubjects(subjectsList);
            for (int i = 0; i < subjects.Length; i++)
            {
                if (subjects[i] != null && subjects[i].StartsWith("бо"))
                    subjects[i] = subjects[i].Remove(0, 3);
            }

            for (int i = 0; i < subjects.Length; i++)
            {
                if (subjects[i] != null)
                {
                    if (!Subjects.Exists((sub) => sub.Name == subjects[i]))
                    {
                        Subjects.Add(new Subject(idSubject, subjects[i]));
                        SpecSubs.Add(new SpecSub(idSpecSub, idSpeciality, idSubject));
                        idSubject++;
                    }

                    else
                    {
                        SpecSubs.Add(new SpecSub(idSpecSub, idSpeciality, Subjects.Find((sub) => sub.Name == subjects[i]).ID));
                    }
                    idSpecSub++;
                }
            }
        }

        private string[] GetSubjects(string subjectsHtml)
        {
            string[] sub = subjectsHtml.Split('(', ')');
            string[] subNames = new string[sub.Length / 2];
            for (int i = 0; i < subNames.Length; i++)
            {
                if(sub[2 * i] != " ")
                    subNames[i] = sub[2 * i].Remove(0,2);
            }
            return subNames;
        }

        private void ParseSpecialities()
        {
            specialitiesNodes = RetreiveNodes(nodesXPaths["SpecialitiesNodes"]);

            if (specialitiesNodes != null)
            {
                GetInfo(districtID, universityID, district, universityNode, specialitiesNodes, nodesXPaths);
            }
        }

        private void ParseUniverities()
        {
            universitiesNodes = RetreiveNodes(nodesXPaths["UniversitiesTypesNode"]);

            if (universitiesNodes != null)
            {
                foreach (HtmlNode univ in universitiesNodes)
                {
                    //some university links return 404 code
                    if (ErrorsLog.IsAvailable(indexPage + "/" + DateTime.Now.Year.ToString() + univ.Attributes["href"].Value.Remove(0, 1)))
                    {
                        ChangeUrl(indexPage + "/" + DateTime.Now.Year.ToString() + univ.Attributes["href"].Value.Remove(0, 1));
                        universityNode = RetreiveNode(nodesXPaths["UniversitiesNode"]);
                        ParseSpecialities();
                    }
                    universityID++;
                    Console.WriteLine(universityID);
                }
            }
        }

        public string StartParsing()
        {           
            districtNodes = RetreiveNodes(nodesXPaths["DistrictsNode"]);
            foreach (HtmlNode node in districtNodes)
            {
                district = node.InnerText;
                if (district == "Вінницька область")
                {
                if (node.InnerText != string.Empty)
                {
                    ChangeUrl(indexPage + node.Attributes["href"].Value);
                    ParseUniverities();
                    return String.Format("District {0}", district);
                }
                }
                
                districtID++;
            }
            return String.Format("Parsing completed");
        }
    }
}