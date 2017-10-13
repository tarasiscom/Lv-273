using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstParser.Core.Habra
{
    class UniverListSettings : IParcerSettings
    {
        public UniverListSettings(string prefix)
        {
            Prefix = prefix;
        }
        public string BaseUrl { get; set; } = "http://www.vstup.info";//set new base URL
        public string Prefix { get; set; } 
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
