using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Parsing
{
    //helper for serialize dictionary
    public class Node
    {
        [XmlAttribute]
        public string key;
        [XmlAttribute]
        public string value;
    }

    class Serializer
    {

        Dictionary<string, string> nodesPaths = new Dictionary<string, string>();

        private void FillDictionary()
        {
            nodesPaths.Add("DistricstNodes", "//table[@id='abet']/tbody/tr/td/a");
            //University, institute etc.
            nodesPaths.Add("UniversitiesTypesNodes", "//table[@id='vnzt0']/tbody/tr/td/a | //table[@id='vnzt1']/tbody/tr/td/a | //table[@id='vnzt2']/tbody/tr/td/a");
            nodesPaths.Add("UniversitiesNodes", "//div/table[@id='about']");
            nodesPaths.Add("SpecialitiesNodes", "//div[@class = 'tab-content']/div/table/tbody/tr");
            nodesPaths.Add("SpecDirectionNodes","td/span[@title ='Галузь']");
            nodesPaths.Add("SpecSpecNode","td/span[@title ='Спеціальність']");
            nodesPaths.Add("SpecFacNode","td/span[@title ='Факультет']");
        }
       
        public  void Serialize()
        {
            FillDictionary();
            XmlSerializer serializer = new XmlSerializer(typeof(Node[]),
                                 new XmlRootAttribute() { ElementName = "Nodes" });
            using (StreamWriter writer = new StreamWriter(File.Create("xPaths.xml")))
            {
                serializer.Serialize(writer, nodesPaths.Select(kv => new Node() { key = kv.Key, value = kv.Value }).ToArray());
            }          
        }
    }
}
