using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Parsing.DataClasses
{
    public class Subject
    {
        public Subject(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        public int ID { get; }

        public string Name { get; }
    }
}
