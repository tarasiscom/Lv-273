using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsing
{
    class Program
    {
        static void Main(string[] args)
        {
            ParseController parse = new ParseController();
            parse.Start(new ShowInConsole(), new Parser("http://vstup.info"));
            Console.ReadKey();
        }
    }
}
