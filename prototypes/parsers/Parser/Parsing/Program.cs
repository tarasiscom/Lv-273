namespace Parsing
{
    using System;
    using System.Collections.Generic;

    public class Program
    {
        private static void Main(string[] args)
        {
            ParseController parse = new ParseController();

            //Serialization xml nodes
            Serializer serializer = new Serializer();
            serializer.Serialize();
            Dictionary<string, string> nodesXpaths = Deserializer.Deserialize();

            parse.Start(new ShowInConsole(), new Parser("http://vstup.info"), new ErrorsLog(), nodesXpaths);
            Console.ReadKey();
        }
    }
}
