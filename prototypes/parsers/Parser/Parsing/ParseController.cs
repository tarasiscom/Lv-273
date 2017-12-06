using System;

namespace Parsing
{
    internal class ParseController
    {
        private readonly IParser parser;
        private readonly ISaver saver;

        //private Dictionary<string, string> nodesXPaths;
 
        public ParseController(ISaver saver, IParser parser)
        {
            this.parser = parser;
            this.saver = saver;
            //this.indexPage = parser.Url;//url incapsulate into nodesXPaths
            //this.nodesXPaths = nodesXPaths;
        }

        public void Start()
        {
            Console.WriteLine(parser.StartParsing());
            saver.SaveAll(parser.Universities, parser.Directions, parser.Specialities, parser.Subjects, parser.SpecSubs, parser.Districts);
        }       
    }
}
