using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstParser.Core.Habra
{
    class UniverSettings : IParcerSettings
    {
        public UniverSettings(string prefix)
        {
            Prefix = prefix;
        }
        public string BaseUrl { get; set; } = "http://www.vstup.info/2017";//set new BaseURL
        public string Prefix { get; set; } 
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
