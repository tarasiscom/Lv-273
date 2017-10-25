namespace Parsing
{
    using HtmlAgilityPack;
    using System;
    using System.Collections.Generic;
    using Parsing.DataClasses;

    internal class ParseController
    {
        private IParser parser;
        private ISaver saver;
        private IErrorsLog errorsLog;

        string district;

        private int districtID = 1;
        private int universityID = 1;
        private int idDirection = 1;
        private int specialityID = 1;

        HtmlNode universityNode;

        string year = "/2017";
        private string indexPage;
       
        private Dictionary<string, string> nodesXpaths = new Dictionary<string, string>();

        public void Start(ISaver saver, IParser parser, IErrorsLog errorsLog, Dictionary<string, string> nodesXpaths)
        {
            this.parser = parser;
            this.saver = saver;
            this.indexPage = parser.Url;
            this.nodesXpaths = nodesXpaths;
            this.errorsLog = errorsLog;

            GetDataFromVstupInfo();
            Save();
        }

        private void GetDataFromVstupInfo()
        {
            errorsLog.StartLog();
            HtmlNodeCollection districtNodes = parser.RetreiveNodes(nodesXpaths["DistrictsNode"]);
            foreach (HtmlNode node in districtNodes)
            {
                district = node.InnerText;
                
                    if (node.InnerText != string.Empty)
                    {
                        //saver.SaveDistrict(parser.GetDistrict(districtID, node.InnerText));
                        parser.ChangeUrl(indexPage + node.Attributes["href"].Value);
                        StarsProcessUniversities();
                    }
                    districtID++;
            
            }
            errorsLog.EndLog();
        }

        private void StarsProcessUniversities()
        {
            HtmlNodeCollection univercitiesNodes = parser.RetreiveNodes(nodesXpaths["UniversitiesTypesNode"]);

            if (univercitiesNodes != null)
            {
                foreach (HtmlNode univ in univercitiesNodes)
                {                    
                    //some univercity links return 404 code
                    if (ErrorsLog.IsAvailable(indexPage + year + univ.Attributes["href"].Value.Remove(0, 1)))
                    {
                        parser.ChangeUrl(indexPage + year + univ.Attributes["href"].Value.Remove(0, 1));
                        universityNode = parser.RetreiveNode(nodesXpaths["UniversitiesNode"]);
                        StartProcessSpeciality(universityNode);
                    }
                    universityID++;
                }
            }
        }

        private void StartProcessSpeciality(HtmlNode universityNode)
        {
            HtmlNodeCollection specialitiesNodes = parser.RetreiveNodes(nodesXpaths["SpecialitiesNodes"]);

            if (specialitiesNodes != null)
            {
                parser.GetInfo(ref specialityID, ref idDirection, universityID, district, universityNode,  specialitiesNodes, nodesXpaths);
                Console.WriteLine("_______________________________________");
                Console.WriteLine(district + universityID);
                Console.WriteLine(universityNode.SelectSingleNode(nodesXpaths["UniversitiesNamesNode"]).InnerText);
            }
        }

        public void Save()
        {
            saver.SaveUniversities(parser.GetUniversities());
            saver.SaveDirections(parser.GetDirections());
            saver.SaveSpecialities(parser.GetSpecialities());
            saver.SaveAll();
        }
    }
}
