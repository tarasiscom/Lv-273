using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace firstParser.Core.Habra
{
    class MainPageSettings : IParcerSettings
    {
        public MainPageSettings()
        {
            
        }
        public string BaseUrl { get; set; } = "http://www.vstup.info/";
        public string Prefix { get; set; } 
        public int StartPoint { get; set; }
        public int EndPoint { get; set; }
    }
}
