using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace Parsing
{
    class Deserializer
    {
            public static Dictionary<string, string> Deserialize()
            {
                Dictionary<string, string> nodesXpaths;
                XmlSerializer serializer = new XmlSerializer(typeof(Node[]));
                using (StreamReader reader = new StreamReader(GetPath()))
                {
                    nodesXpaths = ((Node[])serializer.Deserialize(reader)).ToDictionary(i => i.Key, i => i.Value);
                }

                return nodesXpaths;
            }

            public static string GetPath()
            {
                return String.Format("{0}\\xPaths.xml", Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));
            }
    }
}
