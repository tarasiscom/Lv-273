using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Parsing
{
    //helper for serialize dictionary
    public class Node
    {
        [XmlAttribute]
        public string Key { get; set; }
        [XmlAttribute]
        public string Value { get; set; }
    }

    class Serializer
    {
        Dictionary<string, string> nodesPaths = new Dictionary<string, string>();

        private void FillDictionary()
        {
            nodesPaths.Add("URL", "http://vstup.info");
            nodesPaths.Add("DistrictsNode", "//table[@id='abet']/tbody/tr/td/a");
            //University, institute etc.
            nodesPaths.Add("UniversitiesTypesNode", "//table[@id='vnzt0']/tbody/tr/td/a | //table[@id='vnzt1']/tbody/tr/td/a | //table[@id='vnzt2']/tbody/tr/td/a");
            nodesPaths.Add("UniversitiesNode", "//div/table[@id='about']");
            nodesPaths.Add("UniversitiesNamesNode", "//tr[1]/td[2]");
            nodesPaths.Add("UniversitiesAdressNode", "//tr[5]/td[2]");
            nodesPaths.Add("UniversitiesWebSitesNode", "//tr[7]/td[2]");
            nodesPaths.Add("SpecialitiesNodes", "//div[@class = 'tab-content']/div[@id = 'd_o_1']/table/tbody/tr");
            nodesPaths.Add("SpecDirectionNode", "td/span[@title ='Галузь']");
            nodesPaths.Add("SpecSpecNode", "td/span[@title ='Спеціальність']");
            nodesPaths.Add("SpecFacNode", "td/span[@title ='Факультет']");

            nodesPaths.Add("SpecPZSONode", "td/span[@title ='повна загальна середня освіта']");
            nodesPaths.Add("SpecEnrolledNode", "td/nobr[@title ='зараховано']");
            nodesPaths.Add("SpecApplicationsNode", "td/span[@title ='всього заяв']");

            nodesPaths.Add("SubjectNode", "td[4]");

            nodesPaths.Add("UniversitiesAdressNode2", "//tr[6]/td[2]");
            nodesPaths.Add("UniversitiesWebSitesNode2", "//tr[8]/td[2]");

        }
       
        public void Serialize()
        {
            FillDictionary();
            XmlSerializer serializer = new XmlSerializer(typeof(Node[]));
            using (StreamWriter writer = new StreamWriter(File.Create("xPaths.xml")))
            {
                serializer.Serialize(writer, nodesPaths.Select(kv => new Node() { Key = kv.Key, Value = kv.Value }).ToArray());
            }          
        }
    }
}
