using System;
using System.Collections.Generic;

namespace Parsing
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Serializer serializer = new Serializer();
            serializer.Serialize();
            Dictionary<string, string> nodeXPaths = Deserializer.Deserialize();
            ParseController parse = new ParseController(new DatabaseSaver(), new Parser("http://vstup.info"), nodeXPaths);
            parse.Start();
            Console.ReadKey();
        }
    }
}
