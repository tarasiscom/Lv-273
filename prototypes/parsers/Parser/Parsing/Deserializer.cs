using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.IO;

namespace Parsing
{
    class Deserializer
    {
            public static Dictionary<string, string> Deserialize()
            {
                Dictionary<string, string> nodesPaths;
                XmlSerializer serializer = new XmlSerializer(typeof(Node[]), 
                                 new XmlRootAttribute() { ElementName = "Nodes" });
                using (StreamReader reader = new StreamReader("xPaths.xml"))
                {
                    nodesPaths = ((Node[])serializer.Deserialize(reader)).ToDictionary(i => i.key, i => i.value);
                }

                return nodesPaths;
            }
			
			 public static string GetPath()
            {
                return String.Format("{0}\\xPaths.xml", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            }
    }
}
