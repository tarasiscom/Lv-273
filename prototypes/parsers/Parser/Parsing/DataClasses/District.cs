using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsing.DataClasses
{
    class District
    {
        public int ID { get; private set; }
        public string Name { get; private set; }

        public District(string name)
        {
            Name = name;
        }
    }
}
