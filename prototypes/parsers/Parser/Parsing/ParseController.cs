using HtmlAgilityPack;
using System;
using System.Collections.Generic;

namespace Parsing
{
    internal class ParseController
    {
        private IParser parser;
        private ISaver saver;
        private string district;
        private HtmlNode universityNode;
        private string year = "/2017";
        private string indexPage;
        private Dictionary<string, string> nodesXPaths;

        public ParseController(ISaver saver, IParser parser, Dictionary<string, string> nodesXPaths)
        {
            this.parser = parser;
            this.saver = saver;
            this.indexPage = parser.Url;
            this.nodesXPaths = nodesXPaths;
        }

        public void Start()
        {
            GetDataFromVstupInfo();
            Save();
        }

        public void Save()
        {
            saver.SaveAll(parser.Universities, parser.Directions, parser.Specialities);
        }


        private void GetDataFromVstupInfo()
        {
            int districtID = 1;
            HtmlNodeCollection districtNodes = parser.RetreiveNodes(nodesXPaths["DistrictsNode"]);
            foreach (HtmlNode node in districtNodes)
            {
                district = node.InnerText;
                if (district == "Полтавська область")
                {
                    if (node.InnerText != string.Empty)
                    {
                        parser.ChangeUrl(indexPage + node.Attributes["href"].Value);
                        StartProcessUniversity();
                    }
                }
                districtID++;
            }
        }

        private void StartProcessUniversity()
        {
            HtmlNodeCollection universitiesNodes = parser.RetreiveNodes(nodesXPaths["UniversitiesTypesNode"]);
            int universityID = 1;
            if (universitiesNodes != null)
            {
                foreach (HtmlNode univ in universitiesNodes)
                {
                    //some university links return 404 code
                    if (ErrorsLog.IsAvailable(indexPage + year + univ.Attributes["href"].Value.Remove(0, 1)))
                    {
                        parser.ChangeUrl(indexPage + year + univ.Attributes["href"].Value.Remove(0, 1));
                        universityNode = parser.RetreiveNode(nodesXPaths["UniversitiesNode"]);
                        StartProcessSpeciality(universityNode, universityID);
                    }
                    universityID++;
                }
            }
        }

        private void StartProcessSpeciality(HtmlNode universityNode, int universityID)
        {
            HtmlNodeCollection specialitiesNodes = parser.RetreiveNodes(nodesXPaths["SpecialitiesNodes"]);

            if (specialitiesNodes != null)
            {
                parser.GetInfo(universityID, district, universityNode, specialitiesNodes, nodesXPaths);
                Console.WriteLine("_______________________________________");
                Console.WriteLine(district + universityID);
                Console.WriteLine(universityNode.SelectSingleNode(nodesXPaths["UniversitiesNamesNode"]).InnerText);
            }
        }
    }
}
