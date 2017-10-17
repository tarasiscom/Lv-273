namespace Parsing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Collections;
    using HtmlAgilityPack;

    internal class ParseController
    {
        private IParser parser;
        private ISaver saver;
        private IErrorsLog errorsLog;

        private int districtID = 1;
        private int universityID = 1;

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
        }

        private void GetDataFromVstupInfo()
        {
            errorsLog.StartLog();
            HtmlNodeCollection districtNodes = parser.RetreiveNodes(nodesXpaths["DistricstNode"]);
            foreach (HtmlNode node in districtNodes)
            {
                //After "Дніпропетровська" and "Одеcька" node.InnerText == "";
                //In this places empty node <a></a>
                if (node.InnerText != string.Empty)
                {
                    saver.SaveDistrict(parser.GetDistrict(districtID, node.InnerText));
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

            //"АР Крим", "м.Севастополь" is null
            //These sections doesn't contains any records
            if (univercitiesNodes != null)
            {
                foreach (HtmlNode univ in univercitiesNodes)
                {                    
                    //some univercity links return 404 code
                    if (Parser.IsAvailable(indexPage + year + univ.Attributes["href"].Value.Remove(0, 1)))
                    {
                        parser.ChangeUrl(indexPage + year + univ.Attributes["href"].Value.Remove(0, 1));
                        HtmlNode universityNode = parser.RetreiveNode(nodesXpaths["UniversitiesNode"]);
                        saver.SaveUniversity(parser.GetUniversityInfo(universityID, districtID, universityNode.SelectSingleNode(nodesXpaths["UniversitiesNamesNode"]).InnerText,
                                                                        universityNode.SelectSingleNode(nodesXpaths["UniversitiesAdressNode"]).InnerText, 
                                                                        universityNode.SelectSingleNode(nodesXpaths["UniversitiesWebSitesNode"]).InnerText));
                        StartProcessSpeciality();
                    }
                    universityID++;
                }
            }
        }

        private void StartProcessSpeciality()
        {
            HtmlNodeCollection specialitiesNodes = parser.RetreiveNodes(nodesXpaths["SpecialitiesNodes"]);

            if (specialitiesNodes != null)
            {
                saver.SaveSpecialities(parser.GetSpecialityInfo(specialitiesNodes, nodesXpaths));
                Console.WriteLine("_______________________________________");
            }
        }
    }
}
